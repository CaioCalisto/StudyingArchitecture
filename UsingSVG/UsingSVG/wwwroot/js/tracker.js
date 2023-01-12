
window.alertMessage = (message) => {
    alert(message);
    return true;
}

export function showAlert(message) {
    alert(message);
}

window.manipulateSVG = () => {
    // test to confirm we can manipulate the SVG
    var myMap = SVG('#mySVG');
    myMap.size(200, 300);
}