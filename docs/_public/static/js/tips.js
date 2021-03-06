var currentTip = null;
var currentTipElement = null;

function hideTip(evt, name, unique) {
    var el = document.getElementById(name);
    el.style.display = "none";
    currentTip = null;
}

function findPos(obj) {
    var pos = obj.getBoundingClientRect();
    console.log(obj, pos)
    return [pos.x + 10, pos.y + 30];
}

function hideUsingEsc(e) {
    if (!e) { e = event; }
    hideTip(e, currentTipElement, currentTip);
}

function showTip(evt, name, unique, owner) {
    document.onkeydown = hideUsingEsc;
    if (currentTip == unique) return;
    currentTip = unique;
    currentTipElement = name;

    var pos = findPos(owner ? owner : (evt.srcElement ? evt.srcElement : evt.target));
    var posx = pos[0];
    var posy = pos[1];

    var el = document.getElementById(name);
    var parent = (document.documentElement == null) ? document.body : document.documentElement;
    el.style.position = "fixed";
    el.style.left = posx + "px";
    el.style.top = posy + "px";
    el.style.display = "block";
}