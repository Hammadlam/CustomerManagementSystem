$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    // 'tblAttendance' woh ID hai jo humne HTML table ko di hai
    $('#tblAttendance').DataTable({
        "ajax": {
            "url": "/Attendance/GetAll", // Controller ka URL yahan ayega
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "employeeName", "width": "15%" }, // Model property names match hone chahiye
            { "data": "timeIn", "width": "15%" },
            { "data": "timeOut", "width": "15%" },
            { "data": "breakIn", "width": "10%" },
            { "data": "breakOut", "width": "10%" },
            {
                "data": "present",
                "render": function (data) {
                    if (data == true) {
                        return '<span class="badge bg-success">Present</span>';
                    } else {
                        return '<span class="badge bg-danger">Absent</span>';
                    }
                },
                "width": "10%"
            },
            {
                "data": "isManual",
                "render": function (data) {
                    return data ? "Yes" : "No";
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Attendance/Edit?id=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a onClick=Delete('/Attendance/Delete/${data}') class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>
                    `
                },
                "width": "20%"
            }
        ]
    });
}