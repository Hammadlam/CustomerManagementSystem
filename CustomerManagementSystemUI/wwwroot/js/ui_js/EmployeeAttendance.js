$(document).ready(function () {
    loadEmployeeDropdown();
    loadDataTable();
});
function loadDataTable() {
    $('#tblAttendance').DataTable({
        destroy: true,  
        ajax: {
            url: '/EmployeeAttendance/GetAllAttendance',
            type: 'GET',
            dataSrc: '',
            error: function (xhr) {
                console.error("DataTable Ajax Error:", xhr.responseText);
                alert("Failed to load attendance data");
            }
        },
        columns: [
            {
                data: 'fkUserId',
                render: data => 'Employee #' + data
            },
            {
                data: 'timeIn',
                render: data => data ? moment(data).format('HH:mm') : '-'
            },
            {
                data: 'timeOut',
                render: data => data ? moment(data).format('HH:mm') : '-'
            },
            {
                data: 'breakIn',
                render: data => data ? moment(data).format('HH:mm') : '-'
            },
            {
                data: 'breakOut',
                render: data => data ? moment(data).format('HH:mm') : '-'
            },
            {
                data: 'present',
                render: data =>
                    data
                        ? '<span class="badge bg-success">Present</span>'
                        : '<span class="badge bg-danger">Absent</span>'
            },
            {
                data: 'isManual',
                render: data =>
                    data
                        ? '<span class="badge bg-warning">Yes</span>'
                        : '<span class="badge bg-secondary">No</span>'
            },
            {
                data: 'attendanceId',
                render: data => `
                    <div class="btn-group">
                        <a href="/EmployeeAttendance/Edit?id=${data}"
                           class="btn btn-sm btn-primary">
                           ✏️
                        </a>
                        <button onclick="deleteAttendance(${data})"
                                class="btn btn-sm btn-danger">
                           🗑
                        </button>
                    </div>`
            }
        ]
    });
}

//function loadDataTable() {
//    $('#tblAttendance').DataTable({
//        "ajax": {
//            "url": "/EmployeeAttendance/GetAllAttendance",
//            "type": "GET",
//            "datatype": "json",
//            "dataSrc": ""
//        },
//        "columns": [
//            {
//                "data": "fkUserId",
//                "width": "15%",
//                "render": function (data) {
//                    return "Employee #" + data; 
//                }
//            },
//            {
//                "data": "timeIn",
//                "width": "15%",
//                "render": function (data) {
//                    return data ? moment(data).format("HH:mm") : "-";
//                }
//            },
//            {
//                "data": "timeOut",
//                "width": "15%",
//                "render": function (data) {
//                    return data ? moment(data).format("HH:mm") : "-";
//                }
//            },
//            {
//                "data": "breakIn",
//                "width": "10%",
//                "render": function (data) {
//                    return data ? moment(data).format("HH:mm") : "-";
//                }
//            },
//            {
//                "data": "breakOut",
//                "width": "10%",
//                "render": function (data) {
//                    return data ? moment(data).format("HH:mm") : "-";
//                }
//            },
//            {
//                "data": "present",
//                "width": "10%",
//                "render": function (data) {
//                    return data
//                        ? '<span class="badge bg-success">Present</span>'
//                        : '<span class="badge bg-danger">Absent</span>';
//                }
//            },
//            {
//                "data": "isManual",
//                "width": "5%",
//                "render": function (data) {
//                    return data
//                        ? '<span class="badge bg-warning">Yes</span>'
//                        : '<span class="badge bg-secondary">No</span>';
//                }
//            },
//            {
//                "data": "attendanceId",
//                "width": "20%",
//                "render": function (data) {
//                    return `
//                        <div class="btn-group" role="group">
//                            <a href="/EmployeeAttendance/Edit?id=${data}" class="btn btn-sm btn-primary">
//                                <i class="bi bi-pencil-square"></i>
//                            </a>
//                            <button onclick="deleteAttendance(${data})" class="btn btn-sm btn-danger">
//                                <i class="bi bi-trash-fill"></i>
//                            </button>
//                        </div>
//                    `;
//                }
//            }
//        ]
//    });
//}
function deleteAttendance(id) {
    if (!confirm("Are you sure you want to delete this record?"))
        return;

    $.ajax({
        url: '/EmployeeAttendance/DeleteAttendance?id=' + id,
        type: 'DELETE',
        success: function () {
            $('#tblAttendance').DataTable().ajax.reload();
            alert("Attendance deleted successfully");
        },
        error: function () {
            alert("Failed to delete record");
        }
    });
}
function loadEmployeeDropdown() {
    $.ajax({
        url: '/EmployeeAttendance/GetUsers',
        type: 'GET',
        success: function (data) {
            var ddl = $('#ddlEmployees');
            ddl.empty(); 
            ddl.append('<option value="">-- Select Employee --</option>');

            $.each(data, function (i, user) {
                ddl.append(`<option value="${user.userId}">${user.fullName}</option>`);
            });
        },
        error: function () {
            alert('Failed to load employees');
        }
    });
}

