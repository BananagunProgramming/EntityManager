(function(win, $) {

    function toggleGroupManage() {
        $('#manage-general').toggle();
        $('#manage-general-edit').toggle();
    };

    $('#group-edit').on('click', function () {
        toggleGroupManage();
    });

    $('#group-cancel').on('click', function() {
        toggleGroupManage();
    });

})(window, $);
