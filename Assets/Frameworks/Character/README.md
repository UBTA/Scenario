# About

`Scenario.Frameworks.Character` is a module based package used to create characters under commands control.

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

Interface `IHittable` declares hit catching possibility for characters.
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
> Module works with `LookConstraint`, which should have full setup in editor includig `sourceObjects[0]`.

# Puppet

`Puppet` stands for universal access to the different modules. On character `Init` new `Puppet` instance will be created and it collects all modules under `Character` in hierarhy, after that `Puppet` will be ready to runtime session on character.

```cs
  public class Puppet
  {
    public Transform transform;
    public T Module<T>() where T : ICharacterModule;
    public void Fixed();
    public void Late();
  }
```

By `Module()` call you can get any module declared for character under `Puppet` control. `Fixed()` and `Late()` methods stands for update certain modules that support update types `IFixed` and `ILate`.

>[!NOTE]
>`Module()` call can return types with casts so that for examle you have `SimpleRBMovable` module you can get it using generic of not only `SimpleRbMovable` type, but using `MovableModuleBase` or `IMovable` also. Keep in mind that hierharhy sorting of modules affects their dictionary places so if `Imovable` is first in hierarhy it will be also first in dictionary, and if you ask for `ICharacterModule` you will get it with `IMovable` underlaying type.

# Behaviours

Behaviors is a objects that commands to Puppet how Character what should do during its lifetime.

```cs
  public interface ICommand
  {
    public bool Await { get; set; }
    UniTask Execute(Puppet puppet, CancellationToken token);
  }
```

You have base interface of `ICommand` execution of command will be called automatilcy as soon as you ask `Character` to use it, as you can see you have access to `Puppet` inside command, using this instance you can describe what character should do while executing commands.

You also have basic `Command` whith chaining calls.

```cs
  var behaviour = 
  new Command()
    .Add(new Command())
    .Add(new Command());
```

Using chaining you can create complex baheviour patterns.

Instances that can use rules is marked with `IBehaviourUser` interface.

```cs
  public interface IBehaviourUser
  {
    UniTask Behave(BehaviourTree tree, CancellationToken token = default);
    UniTask Behave(ICommand command, CancellationToken token = default);
  }
```

`Behave()` calls will start command execution for instance.

With package you get all basic command for Modules described earlier, find them within namespace `FAwesome.ScenarioCore.CharacterFramework.Behaviour`.

>[!NOTE]
>You can serialize inside Editor commands using `BehaviourTree`. All basic commands support both scripted and serialized creation.

# Rules

Rules stands for for simulation of external affectors or Character properties. If you want your character to have possibility of getting damage and death after getting lethal damage you use rules to discribe that behaviour.

For Rules you get base interface as for Commands.

```cs
  public interface IRule
  {
    public void Start(Puppet puppet, RuleBook rules);
    public void Check();
    public void Stop();
  }
```

By names you can understand that `Start()` stays for initialisation of rule, so when Character start using this rule `Start()` will be called, same for `Stop()` call, questions may be whith `Check()` method, it will be called as inner update for check rules conditions, with `RulesCheckStep` delta, it will be useful if you want for example to enable different Blend Tree of animations while your `Character` on the sand surface.

Instances that can use rules is marked with `IRuleUser` interface.

```cs
  public interface IRuleUser
  {
    public IRuleUser AddRule(IRule rule);
    public IRuleUser SetRuleBook(RuleBook rules);
    public IRuleUser RemoveRule<R>() where R : IRule;
  }
```

>[!NOTE]
>`Character` at first will create RuleBook for itself on Initialization.

By using `AddRule()` you as you can mention add rule to the user, simple as rock, same with `RemoveRule()`.

>[!NOTE]
>With `RemoveRule()` call you should use generic type, this used for sequre that rule book will have only on instances of certain rule type at once. Somthing like Singleton pattern but inside `RuleBook`.

>[!IMPORTANT]
>All rules applied on character will be stored in inner RuleBook as mentioned earlier and you can set for character whole Book at once by SetRuleBook call it will immediately stop current rules check, and start new once.

# Character

```cs
  public class Character : Instance, IBehaviourUser, IRuleUser
  {
    [SerializeField] private float RulesCheckStep = 0.1f;
        
    protected virtual void FixedUpdate() => Puppet.Fixed();
    protected virtual void LateUpdate() => Puppet.Late();

    public UniTask Behave(BehaviourTree tree, CancellationToken token);
    public UniTask Behave(ICommand command, CancellationToken token);
        
    public IRuleUser AddRule(IRule rule);
    public IRuleUser SetRuleBook(RuleBook rules);
    public IRuleUser RemoveRule<R>() where R : IRule;
  }
```

`Character` as you can see simple, its inherits possibility of behaviour with commands, and listening for rules, all the same as in the real life.

>[!NOTE]
>`Character` inherits `Instance` so it has possibility to be `Inited` and `Ended` as runtime instance of scenario systems.
