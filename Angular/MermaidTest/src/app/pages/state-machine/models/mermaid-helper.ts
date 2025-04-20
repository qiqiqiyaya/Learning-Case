import { StateMachine } from "./state-machine";

export class MermaidHelper {
    static generate(sm: StateMachine): string {
        if (sm.StateConfiguration.length == 0) return '';

        let mermaidString = '';
        sm.StateConfiguration.forEach(sr => {
            sr.Transitions.forEach(tr => {

                if (tr.DestinationState) {
                    mermaidString += `    ${sr.State} --> `;
                    if (tr.Trigger) {
                        mermaidString += `| ${tr.Trigger} |`;
                    }
                    mermaidString += ` ${tr.DestinationState} \n`;
                }
            });
        });

        if (mermaidString == '') return mermaidString;

        mermaidString = 'flowchart TD \n' + mermaidString;
        return mermaidString;
    }
}
