// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function buttonState() {
    if ($('#commentText').val() == '') {
        $('#addComment').attr('disabled', 'disabled');
    } else {
        $('#addComment').removeAttr('disabled'); 
    }
}

$(function () {
    $('#addComment').attr('disabled', 'disabled');
    $('input').change(buttonState);
})