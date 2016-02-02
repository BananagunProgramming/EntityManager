var GroupManagement = function () {

    init = function () {
        editGroup();
        cancel();
        saveGroup();
    };

    editGroup = function () {
        $('#group-edit').on('click', function () {
            toggleGroupManage();
            return false;
        });
    }

    cancel = function () {
        $('#group-cancel').on('click', function () {
            toggleGroupManage();
        });
    }

    saveGroup = function () {
        $('#group-save').on('click', function () {
            var _this = this;
            var url = "/Group/ManageGeneral/";
            //var postData = AddAntiForgeryToken() +'&' + $('#generalform').serialize();
            var postData = $('#generalform').serialize();
            $.ajax({
                type: "POST",
                url: url,
                data: postData,
                success: function (response) {
                    if (response != null && !response.success) {
                        $('#result').html(response.responseText);
                    } else {
                        $('#result').html(response.responseText);
                        init();
                        
                    }
                }
            });
        });
    }

    toggleGroupManage = function () {
        $('#manage-general').toggle();
        $('#manage-general-edit').toggle();
    };

    return {
        init: init
    };
}();
