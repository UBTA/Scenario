# Modules

>[!IMPORTANT]
>Base interfaces used to declare and sort modules by functionality into characters.
---
## `ICharacterModule`

```cs
  public interface ICharacterModule
  {
    void Enable();
    void Disable();
  }
```

Interface for declaring `Enable()` and `Disable()` methods into modules.

---
## `IFixed`

```cs
  public interface IFixed
  {
    void Fixed(Puppet puppet);
  }
```

Interface for getting simple `FixedUpdate()` calls into modules.

---
## `ILate`

```cs
  public interface ILate
  {
    void Late(Puppet puppet);
  }
```

Interface for getting simple `LateUpdate()` calls into modules.

---
## `IMovable`

  ```cs
  public interface IMovable : ICharacterModule, IFixed
  {
    Vector3 Velocity { get; }
    Vector3 Look { get; }
    bool IsGrounded { get; }
  
    float Speed { set; get; }
    bool Gravity { set; get; }

    void CutLookY(bool condition);
    void MoveTargetLook();
    void SetLookTarget(Transform target);
    void SetLookTarget(Vector3? target);
    void SetHardLockMoveTarget(Transform target);
    void SetMoveTarget(Transform target);
    void SetMoveTarget(Vector3? target);
  }
  ```

Interface `IMovable` declares movement behaviour of `Character` instance. Generally `IMovable` functionality should be simple to understand except these: 

> [!NOTE]
> `CutLookY(bool condition)` will ignore X and Z look axis so that your character will will use look only with Y axis.

> [!NOTE]
> After calling `MoveTargetLook()` the character will ignore already set or future set look targets and will use move targets as the look targets.

> [!NOTE]
> Using `SetHardLockMoveTarget()` will lock character position and rotation to the provided Transform. To turn this off you can call it with null target.

Package have basic realisations of `IMovable` is abstract mono `MovableModuleBase` and `SimpleRBMovable`.

---
## `IAnimatable`

```cs
  public interface IAnimatable : ICharacterModule, IFixed
  {
    UniTask Use(AnimatorState state, CancellationToken token);
  }
```
Interface `IAnimatable` declares Unity's Animator usement by `Character` instance. The module is designed to use only Boolean animator switches so that AnimatorState hides bool statement Key and its usabilty.
`AnimatorState` is abstract class so that you should inharit it to declare wanted Animator statement, `Automatic` property will force animator to this state and exits it after the `Duration` has expired, also you
can use it manualy bu changing `Start` and `End` properties before `Play()` call.

Package have basic realisations of `IAnimatable` is abstract mono `AnimatableModuleBase` and `SimpleHumanoidAnimatable`.

> [!NOTE]
> `SimpleHumanoidAnimatable` also can use `IMovable` to get real instance velocity and animate it from animator tree. `IAnimatable` in that case should be after `IMovable` in hierarhy to proper `IFixed` sequence calls.

---
## `IHittable`

```cs
  public interface IHittable : ICharacterModule
  {
    public event Action<Hit> OnHit;    
    public void ForceHit(Vector3? source = null);
  }
```

Interface `IHittable` declares hit cathing possibility for characters.
Module uses `PhysActivator` to catch external hits via calling `RegisterHit()` for collisions.

```cs
  public class PhysActivator : MonoBehaviour
  {
    public event Action<Hit> OnHit;    
    public Collider Col;
    public Rigidbody RB;
    public string Part;
    public bool Kinematiс { set; }

    public void RegisterHit(Vector3 pos, Vector3 dir);
  }
```

> [!NOTE]
> Use `PhysActivator.Part` to determine and filter activators, for example to split activators into bodypars hands, legs, chest etc.

In `OnHit` used for transfer hit data through hittable module, data contains properties for let module know where in which direction and what Activator was hitted.
Package have basic realisations of `IHittable` is abstract mono `HittableModuleBase` and its inheritors `DestructHittable`, `RagDollHittable`.

> [!NOTE]
> `HittableModuleBase` serializes `fallBack` property of type `PhysActivator`, thats used for to determine `ForceHit()` physic impacts.

---
## `IIKHolder`

> [!WARNING]
> `IIKHolder` module uses Unity Technologies `Animation Rigging` asset. You can install it vie `Package Manager`.

```cs
  public interface IIKHolder : ICharacterModule, ILate
  {
    public Grab Left { get; }
    public Grab Right { get; }
    public Grab TwoHand { get; }

    public void Hold(IHolderItem item, bool left = true, bool right = true);
    public void Drop(bool left = true, bool right = true);
  }
```

Interface `IIKHolder` declares possibility of holding items for characters. Its uses `Grab` for setting and parenting gameObjects into character.
Methods `Hold()` and `Drop()` used to grab `IHolderItem` and determing how character should hold this item in hands.

> [!NOTE]
>`Grab` contains propertioes for rooting items into Rig `root` with Vector3 `offset` and `angles` declared in editor vie Inspector or runtime via `ApplyGrab()` which applays current `offset` and `angles` to item.

---
## `IIKLook`

> [!WARNING]
> `IIKLook` module uses Unity Technologies `Animation Rigging` asset. You can install it vie `Package Manager`.

```cs
  public interface IIKLook : ICharacterModule, ILate
  {
    public void Look(Transform target);
    public void Look(Vector3? point);
  }
```

Interface `IIKLook` allows usement of procedural look for characters. It's simply uses Rigging constraints for looking and following the targets.

> [!NOTE]
>Module works with `LookConstraint`, which should have full setup in editor includig `sourceObjects[0]`.

# Puppet

# Behaviours

# Rules

# Character
