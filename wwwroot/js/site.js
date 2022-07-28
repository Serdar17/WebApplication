// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function selectAll(btn) {
    btn.checkValue = (btn.checkValue != "on") ? "on" : "off";
    var value = btn.checkValue == "on";
    var boxes = document.querySelectorAll("table input[type='checkbox']");
    for (var i = 0; i < boxes.length; i++) {
        boxes[i].checked = value;
    }
}