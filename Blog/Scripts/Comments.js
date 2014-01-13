$(function() {
    $('#commentSubmit').click(function () {
        $('#commentSubmit').val("Adding");
        $.post("/home/createComment", $('#commentForm').serialize(), function (data) {
            $('#commentsDiv').append(data);
            $('#commentSubmit').val("Submit");
            $('#commentbody').val('');
        });

        return false;
    });
    
})