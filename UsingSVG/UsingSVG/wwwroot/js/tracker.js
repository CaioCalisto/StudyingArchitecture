
window.alertMessage = (message) => {
    alert(message);
    return true;
}

window.manipulateSVG = () => {
    // test to confirm we can manipulate the SVG
    var myMap = SVG('#mySVG');
    //myMap.size(200, 300);
    var done = myMap.group();
    done.add(getDoneMark());
}

function getDoneMark(){
    return '<g transform="translate(0.000000,1186.000000) scale(0.100000,-0.100000)"\n' +
        'fill="green" stroke="none">\n' +
        '<path d="M12530 11806 c-30 -18 -145 -86 -255 -151 -1135 -671 -2368 -1603\n' +
        '-3585 -2709 -647 -589 -1403 -1342 -2030 -2021 -1085 -1176 -2180 -2523 -3113\n' +
        '-3829 l-48 -67 -32 19 c-18 11 -758 430 -1646 932 -1309 739 -1616 909 -1626\n' +
        '898 -62 -68 -156 -191 -153 -200 2 -6 908 -1061 2014 -2344 l2009 -2333 75 0\n' +
        '76 -1 153 308 c1743 3506 3579 6261 5840 8767 660 731 1624 1701 2381 2394 85\n' +
        '78 154 148 154 156 -2 15 -145 214 -154 214 -3 0 -30 -14 -60 -33z"/>\n' +
        '</g>';
}