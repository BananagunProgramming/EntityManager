(function(win, $) {

    function toggleGroupManage() {
        $('#manage-general').toggle();
        $('#manage-general-edit').toggle();
    };

    $('#group-edit').on('click', function () {
        toggleGroupManage();
        return false
    });

    $('#group-cancel').on('click', function() {
        toggleGroupManage();
    });

    $('#group-save').on('click', function() {
        alert('saving');
    })

})(window, $);
