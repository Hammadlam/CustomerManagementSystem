$(document).ready(function () {
    loadData();
});

function loadData() {
    $.get('/SuperAdmin/GetAllData', function (res) {

        // Load roles dropdown
        $('#roleSelect').empty();
        res.roles.forEach(r => {
            $('#roleSelect').append(`<option value="${r.roleId}">${r.roleName}</option>`);
        });

        // Load users
        let rows = '';
        res.users.forEach(u => {
            rows += `
                <tr>
                    <td>${u.userName}</td>
                    <td>${u.userEmail}</td>
                    <td>${u.roles.join(", ")}</td>
                </tr>`;
        });

        $('#userTable').html(rows);
    });
}

function assignRole() {
    let dto = {
        userId: parseInt($('#userId').val()),
        roleId: parseInt($('#roleSelect').val())
    };

    $.ajax({
        url: '/SuperAdminConsume/AssignRole',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(dto),
        success: function (res) {
            alert(res ? "Assigned" : "Already Exists");
            loadData();
        }
    });
}