import { StateMachine } from "../models/state-machine";

export class PeApproveFlow {
    public static Create(): StateMachine {
        const sm = new StateMachine("Edit");

        sm.Configure("Edit")
            .Permit("Submitted", "FirstApprove");

        sm.Configure("Return")
            .Permit("Rewrite", "Edit");

        sm.Configure("FirstApprove")
            .Permit("FirstApprovedPass", "SecondApprove")
            .Permit("Reject", "Return");

        sm.Configure("SecondApprove")
            .Permit("SecondApprovedPass", "Completed")
            .Permit("Reject", "Return");

        return sm;
    }
}
