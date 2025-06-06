using PChecker;
using PChecker.Runtime;
using PChecker.Runtime.StateMachines;
using PChecker.Runtime.Events;
using PChecker.Runtime.Exceptions;
using PChecker.Runtime.Logging;
using PChecker.Runtime.Values;
using PChecker.Runtime.Specifications;
using Monitor = PChecker.Runtime.Specifications.Monitor;
using System;
using PChecker.SystematicTesting;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 162, 219, 414, 1998
namespace PImplementation
{
}
namespace PImplementation
{
    internal partial class ePickUp : Event
    {
        public ePickUp() : base() {}
        public ePickUp (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new ePickUp();}
    }
}
namespace PImplementation
{
    internal partial class ePutDown : Event
    {
        public ePutDown() : base() {}
        public ePutDown (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new ePutDown();}
    }
}
namespace PImplementation
{
    internal partial class eTaken : Event
    {
        public eTaken() : base() {}
        public eTaken (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new eTaken();}
    }
}
namespace PImplementation
{
    internal partial class eBusy : Event
    {
        public eBusy() : base() {}
        public eBusy (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new eBusy();}
    }
}
namespace PImplementation
{
    internal partial class eThink : Event
    {
        public eThink() : base() {}
        public eThink (IPValue payload): base(payload){ }
        public override IPValue Clone() { return new eThink();}
    }
}
namespace PImplementation
{
    internal partial class eEat : Event
    {
        public eEat() : base() {}
        public eEat (IPValue payload): base(payload){ }
        public override IPValue Clone() { return new eEat();}
    }
}
namespace PImplementation
{
    internal partial class eStart : Event
    {
        public eStart() : base() {}
        public eStart (IPValue payload): base(payload){ }
        public override IPValue Clone() { return new eStart();}
    }
}
namespace PImplementation
{
    internal partial class eDelay : Event
    {
        public eDelay() : base() {}
        public eDelay (IPValue payload): base(payload){ }
        public override IPValue Clone() { return new eDelay();}
    }
}
namespace PImplementation
{
    internal partial class ForkGranted : Event
    {
        public ForkGranted() : base() {}
        public ForkGranted (PNamedTuple payload): base(payload){ }
        public override IPValue Clone() { return new ForkGranted();}
    }
}
namespace PImplementation
{
    internal partial class ForkReleased : Event
    {
        public ForkReleased() : base() {}
        public ForkReleased (PNamedTuple payload): base(payload){ }
        public override IPValue Clone() { return new ForkReleased();}
    }
}
namespace PImplementation
{
    internal partial class Fork : StateMachine
    {
        private PBool inUse = ((PBool)false);
        private PMachineValue holder = null;
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public Fork() {
            this.sends.Add(nameof(ForkGranted));
            this.sends.Add(nameof(ForkReleased));
            this.sends.Add(nameof(eBusy));
            this.sends.Add(nameof(eDelay));
            this.sends.Add(nameof(eEat));
            this.sends.Add(nameof(ePickUp));
            this.sends.Add(nameof(ePutDown));
            this.sends.Add(nameof(eStart));
            this.sends.Add(nameof(eTaken));
            this.sends.Add(nameof(eThink));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(ForkGranted));
            this.receives.Add(nameof(ForkReleased));
            this.receives.Add(nameof(eBusy));
            this.receives.Add(nameof(eDelay));
            this.receives.Add(nameof(eEat));
            this.receives.Add(nameof(ePickUp));
            this.receives.Add(nameof(ePutDown));
            this.receives.Add(nameof(eStart));
            this.receives.Add(nameof(eTaken));
            this.receives.Add(nameof(eThink));
            this.receives.Add(nameof(PHalt));
        }
        
        public void Anon(Event currentMachine_dequeuedEvent)
        {
            Fork currentMachine = this;
            inUse = (PBool)(((PBool)false));
        }
        public  void _handlePickUp(Event currentMachine_dequeuedEvent)
        {
            Fork currentMachine = this;
            handlePickUp((PMachineValue)((Event)currentMachine_dequeuedEvent).Payload);
        }
        public void handlePickUp(PMachineValue p)
        {
            Fork currentMachine = this;
            PBool TMP_tmp0 = ((PBool)false);
            PMachineValue TMP_tmp1 = null;
            Event TMP_tmp2 = null;
            PMachineValue TMP_tmp3 = null;
            PMachineValue TMP_tmp4 = null;
            PMachineValue TMP_tmp5 = null;
            PNamedTuple TMP_tmp6 = (new PNamedTuple(new string[]{"philosopher","fork"},null, null));
            PMachineValue TMP_tmp7 = null;
            Event TMP_tmp8 = null;
            PMachineValue TMP_tmp9 = null;
            TMP_tmp0 = (PBool)(!(inUse));
            if (TMP_tmp0)
            {
                inUse = (PBool)(((PBool)true));
                holder = (PMachineValue)(((PMachineValue)((IPValue)p)?.Clone()));
                TMP_tmp1 = (PMachineValue)(((PMachineValue)((IPValue)p)?.Clone()));
                TMP_tmp2 = (Event)(new eTaken(null));
                TMP_tmp3 = (PMachineValue)(currentMachine.self);
                TMP_tmp2.Payload = TMP_tmp3;
                currentMachine.SendEvent(TMP_tmp1, (Event)TMP_tmp2);
                TMP_tmp4 = (PMachineValue)(((PMachineValue)((IPValue)p)?.Clone()));
                TMP_tmp5 = (PMachineValue)(currentMachine.self);
                TMP_tmp6 = (PNamedTuple)((new PNamedTuple(new string[]{"philosopher","fork"}, TMP_tmp4, TMP_tmp5)));
                currentMachine.Announce((Event)new ForkGranted((new PNamedTuple(new string[]{"philosopher","fork"},null, null))), TMP_tmp6);
            }
            else
            {
                TMP_tmp7 = (PMachineValue)(((PMachineValue)((IPValue)p)?.Clone()));
                TMP_tmp8 = (Event)(new eBusy(null));
                TMP_tmp9 = (PMachineValue)(currentMachine.self);
                TMP_tmp8.Payload = TMP_tmp9;
                currentMachine.SendEvent(TMP_tmp7, (Event)TMP_tmp8);
            }
        }
        public  void _handlePutDown(Event currentMachine_dequeuedEvent)
        {
            Fork currentMachine = this;
            handlePutDown((PMachineValue)((Event)currentMachine_dequeuedEvent).Payload);
        }
        public void handlePutDown(PMachineValue p_1)
        {
            Fork currentMachine = this;
            PBool TMP_tmp0_1 = ((PBool)false);
            PMachineValue TMP_tmp1_1 = null;
            PMachineValue TMP_tmp2_1 = null;
            PNamedTuple TMP_tmp3_1 = (new PNamedTuple(new string[]{"philosopher","fork"},null, null));
            TMP_tmp0_1 = (PBool)((PValues.SafeEquals(holder,p_1)));
            if (TMP_tmp0_1)
            {
                inUse = (PBool)(((PBool)false));
                TMP_tmp1_1 = (PMachineValue)(((PMachineValue)((IPValue)p_1)?.Clone()));
                TMP_tmp2_1 = (PMachineValue)(currentMachine.self);
                TMP_tmp3_1 = (PNamedTuple)((new PNamedTuple(new string[]{"philosopher","fork"}, TMP_tmp1_1, TMP_tmp2_1)));
                currentMachine.Announce((Event)new ForkReleased((new PNamedTuple(new string[]{"philosopher","fork"},null, null))), TMP_tmp3_1);
                holder = (PMachineValue)(null);
            }
        }
        [Start]
        [OnEntry(nameof(Anon))]
        [OnEventDoAction(typeof(ePickUp), nameof(_handlePickUp))]
        [OnEventDoAction(typeof(ePutDown), nameof(_handlePutDown))]
        class Init : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class Philosopher : StateMachine
    {
        private PMachineValue leftFork = null;
        private PMachineValue rightFork = null;
        private PBool hasLeft = ((PBool)false);
        private PBool hasRight = ((PBool)false);
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public Philosopher() {
            this.sends.Add(nameof(ForkGranted));
            this.sends.Add(nameof(ForkReleased));
            this.sends.Add(nameof(eBusy));
            this.sends.Add(nameof(eDelay));
            this.sends.Add(nameof(eEat));
            this.sends.Add(nameof(ePickUp));
            this.sends.Add(nameof(ePutDown));
            this.sends.Add(nameof(eStart));
            this.sends.Add(nameof(eTaken));
            this.sends.Add(nameof(eThink));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(ForkGranted));
            this.receives.Add(nameof(ForkReleased));
            this.receives.Add(nameof(eBusy));
            this.receives.Add(nameof(eDelay));
            this.receives.Add(nameof(eEat));
            this.receives.Add(nameof(ePickUp));
            this.receives.Add(nameof(ePutDown));
            this.receives.Add(nameof(eStart));
            this.receives.Add(nameof(eTaken));
            this.receives.Add(nameof(eThink));
            this.receives.Add(nameof(PHalt));
        }
        
        public void Anon_1(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PTuple payload = (PTuple)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PMachineValue TMP_tmp0_2 = null;
            PMachineValue TMP_tmp1_2 = null;
            PMachineValue TMP_tmp2_2 = null;
            PMachineValue TMP_tmp3_2 = null;
            TMP_tmp0_2 = (PMachineValue)(((PTuple)payload)[0]);
            TMP_tmp1_2 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp0_2)?.Clone()));
            leftFork = TMP_tmp1_2;
            TMP_tmp2_2 = (PMachineValue)(((PTuple)payload)[1]);
            TMP_tmp3_2 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp2_2)?.Clone()));
            rightFork = TMP_tmp3_2;
            hasLeft = (PBool)(((PBool)false));
            hasRight = (PBool)(((PBool)false));
        }
        public void Anon_2(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            currentMachine.RaiseGotoStateEvent<Hungry>();
            return;
        }
        public void Anon_3(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            Event TMP_tmp0_3 = null;
            TMP_tmp0_3 = (Event)(new eDelay(null));
            currentMachine.RaiseEvent(TMP_tmp0_3);
            return;
        }
        public void Anon_4(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_4 = null;
            Event TMP_tmp1_3 = null;
            PMachineValue TMP_tmp2_3 = null;
            TMP_tmp0_4 = (PMachineValue)(((PMachineValue)((IPValue)leftFork)?.Clone()));
            TMP_tmp1_3 = (Event)(new ePickUp(null));
            TMP_tmp2_3 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_3.Payload = TMP_tmp2_3;
            currentMachine.SendEvent(TMP_tmp0_4, (Event)TMP_tmp1_3);
        }
        public void Anon_5(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_5 = null;
            Event TMP_tmp1_4 = null;
            PMachineValue TMP_tmp2_4 = null;
            PMachineValue TMP_tmp3_3 = null;
            Event TMP_tmp4_1 = null;
            PMachineValue TMP_tmp5_1 = null;
            TMP_tmp0_5 = (PMachineValue)(((PMachineValue)((IPValue)leftFork)?.Clone()));
            TMP_tmp1_4 = (Event)(new ePutDown(null));
            TMP_tmp2_4 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_4.Payload = TMP_tmp2_4;
            currentMachine.SendEvent(TMP_tmp0_5, (Event)TMP_tmp1_4);
            TMP_tmp3_3 = (PMachineValue)(((PMachineValue)((IPValue)rightFork)?.Clone()));
            TMP_tmp4_1 = (Event)(new ePutDown(null));
            TMP_tmp5_1 = (PMachineValue)(currentMachine.self);
            TMP_tmp4_1.Payload = TMP_tmp5_1;
            currentMachine.SendEvent(TMP_tmp3_3, (Event)TMP_tmp4_1);
            hasLeft = (PBool)(((PBool)false));
            hasRight = (PBool)(((PBool)false));
        }
        public void Anon_6(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            Event TMP_tmp0_6 = null;
            TMP_tmp0_6 = (Event)(new eDelay(null));
            currentMachine.RaiseEvent(TMP_tmp0_6);
            return;
        }
        public void goHungry()
        {
            Philosopher currentMachine = this;
            currentMachine.RaiseGotoStateEvent<Hungry>();
            return;
        }
        public void goThinking()
        {
            Philosopher currentMachine = this;
            currentMachine.RaiseGotoStateEvent<Thinking>();
            return;
        }
        public  void _onTaken(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            onTaken((PMachineValue)((Event)currentMachine_dequeuedEvent).Payload);
        }
        public void onTaken(PMachineValue f)
        {
            Philosopher currentMachine = this;
            PBool TMP_tmp0_7 = ((PBool)false);
            PMachineValue TMP_tmp1_5 = null;
            Event TMP_tmp2_5 = null;
            PMachineValue TMP_tmp3_4 = null;
            PBool TMP_tmp4_2 = ((PBool)false);
            TMP_tmp0_7 = (PBool)((PValues.SafeEquals(f,leftFork)));
            if (TMP_tmp0_7)
            {
                hasLeft = (PBool)(((PBool)true));
                TMP_tmp1_5 = (PMachineValue)(((PMachineValue)((IPValue)rightFork)?.Clone()));
                TMP_tmp2_5 = (Event)(new ePickUp(null));
                TMP_tmp3_4 = (PMachineValue)(currentMachine.self);
                TMP_tmp2_5.Payload = TMP_tmp3_4;
                currentMachine.SendEvent(TMP_tmp1_5, (Event)TMP_tmp2_5);
            }
            else
            {
                TMP_tmp4_2 = (PBool)((PValues.SafeEquals(f,rightFork)));
                if (TMP_tmp4_2)
                {
                    hasRight = (PBool)(((PBool)true));
                    currentMachine.RaiseGotoStateEvent<Eating>();
                    return;
                }
            }
        }
        public  void _retry(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            retry((PMachineValue)((Event)currentMachine_dequeuedEvent).Payload);
        }
        public void retry(PMachineValue f_1)
        {
            Philosopher currentMachine = this;
            PBool TMP_tmp0_8 = ((PBool)false);
            PBool TMP_tmp1_6 = ((PBool)false);
            PMachineValue TMP_tmp2_6 = null;
            Event TMP_tmp3_5 = null;
            PMachineValue TMP_tmp4_3 = null;
            PBool TMP_tmp5_2 = ((PBool)false);
            PBool TMP_tmp6_1 = ((PBool)false);
            PMachineValue TMP_tmp7_1 = null;
            Event TMP_tmp8_1 = null;
            PMachineValue TMP_tmp9_1 = null;
            TMP_tmp1_6 = (PBool)(((PBool)((IPValue)hasLeft)?.Clone()));
            if (TMP_tmp1_6)
            {
                TMP_tmp0_8 = (PBool)(!(hasRight));
                TMP_tmp1_6 = (PBool)(((PBool)((IPValue)TMP_tmp0_8)?.Clone()));
            }
            if (TMP_tmp1_6)
            {
                TMP_tmp2_6 = (PMachineValue)(((PMachineValue)((IPValue)leftFork)?.Clone()));
                TMP_tmp3_5 = (Event)(new ePutDown(null));
                TMP_tmp4_3 = (PMachineValue)(currentMachine.self);
                TMP_tmp3_5.Payload = TMP_tmp4_3;
                currentMachine.SendEvent(TMP_tmp2_6, (Event)TMP_tmp3_5);
                hasLeft = (PBool)(((PBool)false));
            }
            else
            {
                TMP_tmp6_1 = (PBool)(((PBool)((IPValue)hasRight)?.Clone()));
                if (TMP_tmp6_1)
                {
                    TMP_tmp5_2 = (PBool)(!(hasLeft));
                    TMP_tmp6_1 = (PBool)(((PBool)((IPValue)TMP_tmp5_2)?.Clone()));
                }
                if (TMP_tmp6_1)
                {
                    TMP_tmp7_1 = (PMachineValue)(((PMachineValue)((IPValue)rightFork)?.Clone()));
                    TMP_tmp8_1 = (Event)(new ePutDown(null));
                    TMP_tmp9_1 = (PMachineValue)(currentMachine.self);
                    TMP_tmp8_1.Payload = TMP_tmp9_1;
                    currentMachine.SendEvent(TMP_tmp7_1, (Event)TMP_tmp8_1);
                    hasRight = (PBool)(((PBool)false));
                }
            }
            currentMachine.RaiseGotoStateEvent<Thinking>();
            return;
        }
        public  void _startEating(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            startEating();
        }
        public void startEating()
        {
            Philosopher currentMachine = this;
            currentMachine.RaiseGotoStateEvent<Hungry>();
            return;
        }
        [Start]
        [OnEntry(nameof(Anon_1))]
        [OnEventDoAction(typeof(eStart), nameof(_startEating))]
        class Init : State
        {
        }
        [OnEntry(nameof(Anon_2))]
        class ReadyToEat : State
        {
        }
        [OnEntry(nameof(Anon_3))]
        [OnEventGotoState(typeof(eDelay), typeof(ReadyToEat))]
        class Thinking : State
        {
        }
        [OnEntry(nameof(Anon_4))]
        [OnEventDoAction(typeof(eTaken), nameof(_onTaken))]
        [OnEventDoAction(typeof(eBusy), nameof(_retry))]
        class Hungry : State
        {
        }
        [OnEntry(nameof(Anon_5))]
        class FinishEating : State
        {
        }
        [OnEntry(nameof(Anon_6))]
        [OnEventGotoState(typeof(eDelay), typeof(FinishEating))]
        class Eating : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class TestWithNoSwap : StateMachine
    {
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public TestWithNoSwap() {
            this.sends.Add(nameof(ForkGranted));
            this.sends.Add(nameof(ForkReleased));
            this.sends.Add(nameof(eBusy));
            this.sends.Add(nameof(eDelay));
            this.sends.Add(nameof(eEat));
            this.sends.Add(nameof(ePickUp));
            this.sends.Add(nameof(ePutDown));
            this.sends.Add(nameof(eStart));
            this.sends.Add(nameof(eTaken));
            this.sends.Add(nameof(eThink));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(ForkGranted));
            this.receives.Add(nameof(ForkReleased));
            this.receives.Add(nameof(eBusy));
            this.receives.Add(nameof(eDelay));
            this.receives.Add(nameof(eEat));
            this.receives.Add(nameof(ePickUp));
            this.receives.Add(nameof(ePutDown));
            this.receives.Add(nameof(eStart));
            this.receives.Add(nameof(eTaken));
            this.receives.Add(nameof(eThink));
            this.receives.Add(nameof(PHalt));
            this.creates.Add(nameof(I_Fork));
            this.creates.Add(nameof(I_Philosopher));
        }
        
        public void Anon_7(Event currentMachine_dequeuedEvent)
        {
            TestWithNoSwap currentMachine = this;
            PMap forks = new PMap();
            PMap phils = new PMap();
            PInt i = ((PInt)0);
            PMachineValue left = null;
            PMachineValue right = null;
            PMachineValue phil = null;
            PBool TMP_tmp0_9 = ((PBool)false);
            PBool TMP_tmp1_7 = ((PBool)false);
            PMachineValue TMP_tmp2_7 = null;
            PInt TMP_tmp3_6 = ((PInt)0);
            PBool TMP_tmp4_4 = ((PBool)false);
            PBool TMP_tmp5_3 = ((PBool)false);
            PMachineValue TMP_tmp6_2 = null;
            PMachineValue TMP_tmp7_2 = null;
            PInt TMP_tmp8_2 = ((PInt)0);
            PInt TMP_tmp9_2 = ((PInt)0);
            PMachineValue TMP_tmp10 = null;
            PMachineValue TMP_tmp11 = null;
            PMachineValue TMP_tmp12 = null;
            PMachineValue TMP_tmp13 = null;
            PTuple TMP_tmp14 = (new PTuple(null, null));
            PMachineValue TMP_tmp15 = null;
            PInt TMP_tmp16 = ((PInt)0);
            PBool TMP_tmp17 = ((PBool)false);
            PBool TMP_tmp18 = ((PBool)false);
            PMachineValue TMP_tmp19 = null;
            PMachineValue TMP_tmp20 = null;
            Event TMP_tmp21 = null;
            PInt TMP_tmp22 = ((PInt)0);
            i = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp0_9 = (PBool)((i) < (((PInt)(5))));
                TMP_tmp1_7 = (PBool)(((PBool)((IPValue)TMP_tmp0_9)?.Clone()));
                if (TMP_tmp1_7)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp2_7 = (PMachineValue)(currentMachine.CreateInterface<I_Fork>( currentMachine));
                ((PMap)forks)[i] = (PMachineValue)TMP_tmp2_7;
                TMP_tmp3_6 = (PInt)((i) + (((PInt)(1))));
                i = TMP_tmp3_6;
            }
            i = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp4_4 = (PBool)((i) < (((PInt)(5))));
                TMP_tmp5_3 = (PBool)(((PBool)((IPValue)TMP_tmp4_4)?.Clone()));
                if (TMP_tmp5_3)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp6_2 = (PMachineValue)(((PMap)forks)[i]);
                TMP_tmp7_2 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp6_2)?.Clone()));
                left = TMP_tmp7_2;
                TMP_tmp8_2 = (PInt)((i) + (((PInt)(1))));
                TMP_tmp9_2 = (PInt)((TMP_tmp8_2) % (((PInt)(5))));
                TMP_tmp10 = (PMachineValue)(((PMap)forks)[TMP_tmp9_2]);
                TMP_tmp11 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp10)?.Clone()));
                right = TMP_tmp11;
                TMP_tmp12 = (PMachineValue)(((PMachineValue)((IPValue)left)?.Clone()));
                TMP_tmp13 = (PMachineValue)(((PMachineValue)((IPValue)right)?.Clone()));
                TMP_tmp14 = (PTuple)(new PTuple(TMP_tmp12, TMP_tmp13));
                TMP_tmp15 = (PMachineValue)(currentMachine.CreateInterface<I_Philosopher>( currentMachine, TMP_tmp14));
                phil = (PMachineValue)TMP_tmp15;
                ((PMap)phils)[i] = (PMachineValue)(((PMachineValue)((IPValue)phil)?.Clone()));
                TMP_tmp16 = (PInt)((i) + (((PInt)(1))));
                i = TMP_tmp16;
            }
            i = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp17 = (PBool)((i) < (((PInt)(5))));
                TMP_tmp18 = (PBool)(((PBool)((IPValue)TMP_tmp17)?.Clone()));
                if (TMP_tmp18)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp19 = (PMachineValue)(((PMap)phils)[i]);
                TMP_tmp20 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp19)?.Clone()));
                TMP_tmp21 = (Event)(new eStart(null));
                currentMachine.SendEvent(TMP_tmp20, (Event)TMP_tmp21);
                TMP_tmp22 = (PInt)((i) + (((PInt)(1))));
                i = TMP_tmp22;
            }
        }
        [Start]
        [OnEntry(nameof(Anon_7))]
        class Init : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class TestWithSwap : StateMachine
    {
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public TestWithSwap() {
            this.sends.Add(nameof(ForkGranted));
            this.sends.Add(nameof(ForkReleased));
            this.sends.Add(nameof(eBusy));
            this.sends.Add(nameof(eDelay));
            this.sends.Add(nameof(eEat));
            this.sends.Add(nameof(ePickUp));
            this.sends.Add(nameof(ePutDown));
            this.sends.Add(nameof(eStart));
            this.sends.Add(nameof(eTaken));
            this.sends.Add(nameof(eThink));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(ForkGranted));
            this.receives.Add(nameof(ForkReleased));
            this.receives.Add(nameof(eBusy));
            this.receives.Add(nameof(eDelay));
            this.receives.Add(nameof(eEat));
            this.receives.Add(nameof(ePickUp));
            this.receives.Add(nameof(ePutDown));
            this.receives.Add(nameof(eStart));
            this.receives.Add(nameof(eTaken));
            this.receives.Add(nameof(eThink));
            this.receives.Add(nameof(PHalt));
            this.creates.Add(nameof(I_Fork));
            this.creates.Add(nameof(I_Philosopher));
        }
        
        public void Anon_8(Event currentMachine_dequeuedEvent)
        {
            TestWithSwap currentMachine = this;
            PMap forks_1 = new PMap();
            PMap phils_1 = new PMap();
            PInt i_1 = ((PInt)0);
            PMachineValue left_1 = null;
            PMachineValue right_1 = null;
            PMachineValue phil_1 = null;
            PBool TMP_tmp0_10 = ((PBool)false);
            PBool TMP_tmp1_8 = ((PBool)false);
            PMachineValue TMP_tmp2_8 = null;
            PInt TMP_tmp3_7 = ((PInt)0);
            PBool TMP_tmp4_5 = ((PBool)false);
            PBool TMP_tmp5_4 = ((PBool)false);
            PMachineValue TMP_tmp6_3 = null;
            PMachineValue TMP_tmp7_3 = null;
            PInt TMP_tmp8_3 = ((PInt)0);
            PInt TMP_tmp9_3 = ((PInt)0);
            PMachineValue TMP_tmp10_1 = null;
            PMachineValue TMP_tmp11_1 = null;
            PBool TMP_tmp12_1 = ((PBool)false);
            PMachineValue TMP_tmp13_1 = null;
            PMachineValue TMP_tmp14_1 = null;
            PTuple TMP_tmp15_1 = (new PTuple(null, null));
            PMachineValue TMP_tmp16_1 = null;
            PMachineValue TMP_tmp17_1 = null;
            PMachineValue TMP_tmp18_1 = null;
            PTuple TMP_tmp19_1 = (new PTuple(null, null));
            PMachineValue TMP_tmp20_1 = null;
            PInt TMP_tmp21_1 = ((PInt)0);
            PBool TMP_tmp22_1 = ((PBool)false);
            PBool TMP_tmp23 = ((PBool)false);
            PMachineValue TMP_tmp24 = null;
            PMachineValue TMP_tmp25 = null;
            Event TMP_tmp26 = null;
            PInt TMP_tmp27 = ((PInt)0);
            i_1 = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp0_10 = (PBool)((i_1) < (((PInt)(5))));
                TMP_tmp1_8 = (PBool)(((PBool)((IPValue)TMP_tmp0_10)?.Clone()));
                if (TMP_tmp1_8)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp2_8 = (PMachineValue)(currentMachine.CreateInterface<I_Fork>( currentMachine));
                ((PMap)forks_1)[i_1] = (PMachineValue)TMP_tmp2_8;
                TMP_tmp3_7 = (PInt)((i_1) + (((PInt)(1))));
                i_1 = TMP_tmp3_7;
            }
            i_1 = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp4_5 = (PBool)((i_1) < (((PInt)(5))));
                TMP_tmp5_4 = (PBool)(((PBool)((IPValue)TMP_tmp4_5)?.Clone()));
                if (TMP_tmp5_4)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp6_3 = (PMachineValue)(((PMap)forks_1)[i_1]);
                TMP_tmp7_3 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp6_3)?.Clone()));
                left_1 = TMP_tmp7_3;
                TMP_tmp8_3 = (PInt)((i_1) + (((PInt)(1))));
                TMP_tmp9_3 = (PInt)((TMP_tmp8_3) % (((PInt)(5))));
                TMP_tmp10_1 = (PMachineValue)(((PMap)forks_1)[TMP_tmp9_3]);
                TMP_tmp11_1 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp10_1)?.Clone()));
                right_1 = TMP_tmp11_1;
                TMP_tmp12_1 = (PBool)((PValues.SafeEquals(i_1,((PInt)(0)))));
                if (TMP_tmp12_1)
                {
                    TMP_tmp13_1 = (PMachineValue)(((PMachineValue)((IPValue)right_1)?.Clone()));
                    TMP_tmp14_1 = (PMachineValue)(((PMachineValue)((IPValue)left_1)?.Clone()));
                    TMP_tmp15_1 = (PTuple)(new PTuple(TMP_tmp13_1, TMP_tmp14_1));
                    TMP_tmp16_1 = (PMachineValue)(currentMachine.CreateInterface<I_Philosopher>( currentMachine, TMP_tmp15_1));
                    phil_1 = (PMachineValue)TMP_tmp16_1;
                }
                else
                {
                    TMP_tmp17_1 = (PMachineValue)(((PMachineValue)((IPValue)left_1)?.Clone()));
                    TMP_tmp18_1 = (PMachineValue)(((PMachineValue)((IPValue)right_1)?.Clone()));
                    TMP_tmp19_1 = (PTuple)(new PTuple(TMP_tmp17_1, TMP_tmp18_1));
                    TMP_tmp20_1 = (PMachineValue)(currentMachine.CreateInterface<I_Philosopher>( currentMachine, TMP_tmp19_1));
                    phil_1 = (PMachineValue)TMP_tmp20_1;
                }
                ((PMap)phils_1)[i_1] = (PMachineValue)(((PMachineValue)((IPValue)phil_1)?.Clone()));
                TMP_tmp21_1 = (PInt)((i_1) + (((PInt)(1))));
                i_1 = TMP_tmp21_1;
            }
            i_1 = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp22_1 = (PBool)((i_1) < (((PInt)(5))));
                TMP_tmp23 = (PBool)(((PBool)((IPValue)TMP_tmp22_1)?.Clone()));
                if (TMP_tmp23)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp24 = (PMachineValue)(((PMap)phils_1)[i_1]);
                TMP_tmp25 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp24)?.Clone()));
                TMP_tmp26 = (Event)(new eStart(null));
                currentMachine.SendEvent(TMP_tmp25, (Event)TMP_tmp26);
                TMP_tmp27 = (PInt)((i_1) + (((PInt)(1))));
                i_1 = TMP_tmp27;
            }
        }
        [Start]
        [OnEntry(nameof(Anon_8))]
        class Init : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class ForkUsageSpec : Monitor
    {
        private PSet activePhilosophers = new PSet();
        static ForkUsageSpec() {
            observes.Add(nameof(ForkGranted));
            observes.Add(nameof(ForkReleased));
        }
        
        public void Anon_9(Event currentMachine_dequeuedEvent)
        {
            ForkUsageSpec currentMachine = this;
        }
        public void Anon_10(Event currentMachine_dequeuedEvent)
        {
            ForkUsageSpec currentMachine = this;
            PNamedTuple payload_1 = (PNamedTuple)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PMachineValue TMP_tmp0_11 = null;
            PInt TMP_tmp1_9 = ((PInt)0);
            PBool TMP_tmp2_9 = ((PBool)false);
            PString TMP_tmp3_8 = ((PString)"");
            TMP_tmp0_11 = (PMachineValue)(((PNamedTuple)payload_1)["philosopher"]);
            ((PSet)activePhilosophers).Add(TMP_tmp0_11);
            TMP_tmp1_9 = (PInt)(((PInt)(activePhilosophers).Count));
            TMP_tmp2_9 = (PBool)((TMP_tmp1_9) < (((PInt)(5))));
            if (TMP_tmp2_9)
            {
            }
            else
            {
                TMP_tmp3_8 = (PString)(((PString) String.Format("NoDeadLock.p:17:7")));
                currentMachine.Assert(TMP_tmp2_9,"Assertion Failed: " + TMP_tmp3_8);
            }
        }
        public void Anon_11(Event currentMachine_dequeuedEvent)
        {
            ForkUsageSpec currentMachine = this;
            PNamedTuple payload_2 = (PNamedTuple)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PMachineValue TMP_tmp0_12 = null;
            TMP_tmp0_12 = (PMachineValue)(((PNamedTuple)payload_2)["philosopher"]);
            ((PSet)activePhilosophers).Remove(TMP_tmp0_12);
        }
        [Start]
        [OnEntry(nameof(Anon_9))]
        [OnEventDoAction(typeof(ForkGranted), nameof(Anon_10))]
        [OnEventDoAction(typeof(ForkReleased), nameof(Anon_11))]
        class Monitoring : State
        {
        }
    }
}
namespace PImplementation
{
    public class TestCaseNoSwap {
        public static void InitializeLinkMap() {
            PModule.linkMap.Clear();
            PModule.linkMap[nameof(I_Fork)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_Philosopher)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_TestWithNoSwap)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_TestWithNoSwap)].Add(nameof(I_Fork), nameof(I_Fork));
            PModule.linkMap[nameof(I_TestWithNoSwap)].Add(nameof(I_Philosopher), nameof(I_Philosopher));
        }
        
        public static void InitializeInterfaceDefMap() {
            PModule.interfaceDefinitionMap.Clear();
            PModule.interfaceDefinitionMap.Add(nameof(I_Fork), typeof(Fork));
            PModule.interfaceDefinitionMap.Add(nameof(I_Philosopher), typeof(Philosopher));
            PModule.interfaceDefinitionMap.Add(nameof(I_TestWithNoSwap), typeof(TestWithNoSwap));
        }
        
        public static void InitializeMonitorObserves() {
            PModule.monitorObserves.Clear();
            PModule.monitorObserves[nameof(ForkUsageSpec)] = new List<string>();
            PModule.monitorObserves[nameof(ForkUsageSpec)].Add(nameof(ForkGranted));
            PModule.monitorObserves[nameof(ForkUsageSpec)].Add(nameof(ForkReleased));
        }
        
        public static void InitializeMonitorMap(ControlledRuntime runtime) {
            PModule.monitorMap.Clear();
            PModule.monitorMap[nameof(I_Fork)] = new List<Type>();
            PModule.monitorMap[nameof(I_Fork)].Add(typeof(ForkUsageSpec));
            PModule.monitorMap[nameof(I_Philosopher)] = new List<Type>();
            PModule.monitorMap[nameof(I_Philosopher)].Add(typeof(ForkUsageSpec));
            PModule.monitorMap[nameof(I_TestWithNoSwap)] = new List<Type>();
            PModule.monitorMap[nameof(I_TestWithNoSwap)].Add(typeof(ForkUsageSpec));
            runtime.RegisterMonitor<ForkUsageSpec>();
        }
        
        
        [PChecker.SystematicTesting.Test]
        public static void Execute(ControlledRuntime runtime) {
            runtime.RegisterLog(new PCheckerLogTextFormatter());
            runtime.RegisterLog(new PCheckerLogJsonFormatter());
            PModule.runtime = runtime;
            PHelper.InitializeInterfaces();
            PHelper.InitializeEnums();
            InitializeLinkMap();
            InitializeInterfaceDefMap();
            InitializeMonitorMap(runtime);
            InitializeMonitorObserves();
            runtime.CreateStateMachine(typeof(TestWithNoSwap), "TestWithNoSwap");
        }
    }
}
namespace PImplementation
{
    public class TestCaseWithSwap {
        public static void InitializeLinkMap() {
            PModule.linkMap.Clear();
            PModule.linkMap[nameof(I_Fork)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_Philosopher)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_TestWithSwap)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_TestWithSwap)].Add(nameof(I_Fork), nameof(I_Fork));
            PModule.linkMap[nameof(I_TestWithSwap)].Add(nameof(I_Philosopher), nameof(I_Philosopher));
        }
        
        public static void InitializeInterfaceDefMap() {
            PModule.interfaceDefinitionMap.Clear();
            PModule.interfaceDefinitionMap.Add(nameof(I_Fork), typeof(Fork));
            PModule.interfaceDefinitionMap.Add(nameof(I_Philosopher), typeof(Philosopher));
            PModule.interfaceDefinitionMap.Add(nameof(I_TestWithSwap), typeof(TestWithSwap));
        }
        
        public static void InitializeMonitorObserves() {
            PModule.monitorObserves.Clear();
            PModule.monitorObserves[nameof(ForkUsageSpec)] = new List<string>();
            PModule.monitorObserves[nameof(ForkUsageSpec)].Add(nameof(ForkGranted));
            PModule.monitorObserves[nameof(ForkUsageSpec)].Add(nameof(ForkReleased));
        }
        
        public static void InitializeMonitorMap(ControlledRuntime runtime) {
            PModule.monitorMap.Clear();
            PModule.monitorMap[nameof(I_Fork)] = new List<Type>();
            PModule.monitorMap[nameof(I_Fork)].Add(typeof(ForkUsageSpec));
            PModule.monitorMap[nameof(I_Philosopher)] = new List<Type>();
            PModule.monitorMap[nameof(I_Philosopher)].Add(typeof(ForkUsageSpec));
            PModule.monitorMap[nameof(I_TestWithSwap)] = new List<Type>();
            PModule.monitorMap[nameof(I_TestWithSwap)].Add(typeof(ForkUsageSpec));
            runtime.RegisterMonitor<ForkUsageSpec>();
        }
        
        
        [PChecker.SystematicTesting.Test]
        public static void Execute(ControlledRuntime runtime) {
            runtime.RegisterLog(new PCheckerLogTextFormatter());
            runtime.RegisterLog(new PCheckerLogJsonFormatter());
            PModule.runtime = runtime;
            PHelper.InitializeInterfaces();
            PHelper.InitializeEnums();
            InitializeLinkMap();
            InitializeInterfaceDefMap();
            InitializeMonitorMap(runtime);
            InitializeMonitorObserves();
            runtime.CreateStateMachine(typeof(TestWithSwap), "TestWithSwap");
        }
    }
}
// TODO: NamedModule Fork_1
// TODO: NamedModule Philosopher_1
// TODO: NamedModule TestWithNoSwap_1
// TODO: NamedModule TestWithSwap_1
namespace PImplementation
{
    public class I_Fork : PMachineValue {
        public I_Fork (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_Philosopher : PMachineValue {
        public I_Philosopher (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_TestWithNoSwap : PMachineValue {
        public I_TestWithNoSwap (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_TestWithSwap : PMachineValue {
        public I_TestWithSwap (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public partial class PHelper {
        public static void InitializeInterfaces() {
            PInterfaces.Clear();
            PInterfaces.AddInterface(nameof(I_Fork), nameof(ForkGranted), nameof(ForkReleased), nameof(eBusy), nameof(eDelay), nameof(eEat), nameof(ePickUp), nameof(ePutDown), nameof(eStart), nameof(eTaken), nameof(eThink), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_Philosopher), nameof(ForkGranted), nameof(ForkReleased), nameof(eBusy), nameof(eDelay), nameof(eEat), nameof(ePickUp), nameof(ePutDown), nameof(eStart), nameof(eTaken), nameof(eThink), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_TestWithNoSwap), nameof(ForkGranted), nameof(ForkReleased), nameof(eBusy), nameof(eDelay), nameof(eEat), nameof(ePickUp), nameof(ePutDown), nameof(eStart), nameof(eTaken), nameof(eThink), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_TestWithSwap), nameof(ForkGranted), nameof(ForkReleased), nameof(eBusy), nameof(eDelay), nameof(eEat), nameof(ePickUp), nameof(ePutDown), nameof(eStart), nameof(eTaken), nameof(eThink), nameof(PHalt));
        }
    }
    
}
namespace PImplementation
{
    public partial class PHelper {
        public static void InitializeEnums() {
            PEnum.Clear();
        }
    }
    
}
#pragma warning restore 162, 219, 414
