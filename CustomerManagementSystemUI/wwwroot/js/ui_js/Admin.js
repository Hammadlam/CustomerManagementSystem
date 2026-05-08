$(document).ready(function () {
    $.get('/Admin/GetDashboardData', function (data) {
        let rows = '';
        data.forEach(u => {
            rows += `
                    <tr>
                        <td>${u.userName}</td>
                        <td>${u.userEmail}</td>
                        <td>${u.roles.join(", ")}</td>
                    </tr>`;
        });
        $('#adminTable').html(rows);
    });
});