
window.alertMessage = (message) => {
    alert(message);
    return true;
}

window.addDoneMark = (svgFile, xPosition, yPosition) => {
    var myMap = SVG('#mySVG');
    var point = myMap.node.createSVGPoint();
    point.x = xPosition;
    point.y = yPosition;
    var svgCoordinates = point.matrixTransform(myMap.node.getScreenCTM().inverse());
    
    // TODO: this is totally wrong, I need to improve it.
    myMap.image(svgFile).size('2%', '2%').attr({ x: svgCoordinates.x + 172.10, y: svgCoordinates.y - 4 });
}