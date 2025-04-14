

function initTest(){
    mermaid.initialize({
        startOnLoad: false
    });
}

console.log("12222222222");
window.mermaidFunction = function (node, componentId) {
    var event = new CustomEvent("mermaid-click", {
        bubbles: true,
        detail: { node, componentId: Number(componentId) },
    });
    window.dispatchEvent(event);
}
