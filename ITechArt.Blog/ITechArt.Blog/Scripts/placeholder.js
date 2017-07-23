$(document).ready(function () {
    $.ajaxSetup(
        {
            cache: false
        });
    $(".viewDialog").on("click", function (e) {
        e.preventDefault();
        $("<div></div>")
            .addClass("dialog")
            .appendTo("body")
            .dialog({
                title: $(this).attr("data-dialog-title"),
                close: function () { $(this).remove() },
                modal: true
            })
            .load(this.href);
    });
});
function doClear(theText) {
    if (theText.value == theText.defaultValue) {
        theText.value = ""
    }
}
function doDefault(theText) {
    if (theText.value == "") {
        theText.value = theText.defaultValue
    }
}