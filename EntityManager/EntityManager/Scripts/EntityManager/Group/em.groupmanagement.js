var entitymanager;
(function (entitymanager) {
    (function (groupmanagement) {
        //var settings = {};

        var GroupManagement = (function () {

            function GroupManagement() {
                //$.extend(true, settings, opts);
                //var self = this;
            }

            GroupManagement.prototype.init = function () {
                var _this = this;

                $('#group-edit').on('click', function () {
                    toggleGroupManage();
                    return false;
                });

                $('#group-cancel').on('click', function () {
                    toggleGroupManage();
                });

                $('#group-save').on('click', function() {
                    var _this = this;
                    var url = "/Group/ManageGeneral/";
                    var postData = AddAntiForgeryToken() +'&' + $('#generalform').serialize();
                    alert(postData);
                        $.ajax({
                            type: "POST",
                            url: url,
                            data: postData,
                        success: function (result) {
                            _this.$section = $(result).replaceAll($('#generalform'));
                            GroupManagement.prototype.init();
                        },
                    });
                });

                //called methods
                function toggleGroupManage() {
                    $('#manage-general').toggle();
                    $('#manage-general-edit').toggle();
                };
            };

            return GroupManagement;
        })();
        groupmanagement.GroupManagement = GroupManagement;
    })(entitymanager.groupmanagement || (entitymanager.groupmanagement = {}));
})(entitymanager || (entitymanager = {}));

