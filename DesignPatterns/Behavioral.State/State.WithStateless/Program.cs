using System;
using Stateless;

namespace State.WithStateless
{
    enum Health
    {
        NonReproductive, Pregnant, Reproductive
    }

    enum Activity
    {
        GiveBirth, ReachPuberty, HaveAbortion, HaveUnprotectedSex, Historectomy
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var stateMachine = new StateMachine<Health, Activity>(Health.NonReproductive);
            stateMachine.Configure(Health.NonReproductive)
                .Permit(Activity.ReachPuberty, Health.Reproductive);
            stateMachine.Configure(Health.Reproductive)
                .Permit(Activity.Historectomy, Health.NonReproductive)
                .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant,
                    () => ParentsNotWatching);
            stateMachine.Configure(Health.Pregnant)
                .Permit(Activity.GiveBirth, Health.Reproductive)
                .Permit(Activity.HaveAbortion, Health.Reproductive);
        }

        public static bool ParentsNotWatching
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}