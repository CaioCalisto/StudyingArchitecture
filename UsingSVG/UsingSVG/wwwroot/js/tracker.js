
window.alertMessage = (message) => {
    alert(message);
    return true;
}

window.addDoneMark = (svgFile) => {
    var myMap = SVG('#mySVG');
    myMap.image(svgFile).size('50%', '100%');
}