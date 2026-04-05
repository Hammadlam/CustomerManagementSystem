$(document).ready(function () {
    loadEmployeeDropdown();
    loadDataTable();

    // EDIT CLICK
    $(document).on('click', '.edit-btn', function () {
        editRow(this);
    });

    // SAVE CLICK
    $(document).on('click', '.save-btn', function () {
        saveRow(this);
    });

});
function loadDataTable(id = null) {
    $('#tblAttendance').DataTable({
        destroy: true,
        ajax: {
            url: id
                ? '/EmployeeAttendance/GetAttendanceByEmployeeId?id=' + id
                : '/EmployeeAttendance/GetAllAttendance',
            type: 'GET',
            dataSrc: ''
        },
       
        columns: [

            { data: 'fkUserId', className: 'no-edit' },

            { data: 'userName', className: 'no-edit' },

            {
                data: 'attendanceDate',
                className: 'no-edit',
                render: d => d ? moment(d).format('HH:mm') : '-'
            },
            {
                data: 'timeIn',
                className: 'editable-time',
                render: d => d ? moment(d).format('HH:mm') : '-'
            },
            
            {
                data: 'breakIn',
                className: 'editable-time',
                render: d => d ? moment(d).format('HH:mm') : '-'
            },

            {
                data: 'breakOut',
                className: 'editable-time',
                render: d => d ? moment(d).format('HH:mm') : '-'
            },

            {
                data: 'timeOut',
                className: 'editable-time',
                render: d => d ? moment(d).format('HH:mm') : '-'
            },

            //{
            //    data: 'present',
            //    className: 'editable-check',
            //    render: d =>
            //        `<input type="checkbox"
            //    ${d ? 'checked' : ''}
            //    style="accent-color:#0d6efd; pointer-events:none;">`
            //},

          

            {
                data: null,
                className: 'no-edit action-col',
                orderable: false,
                render: (data, type, row) => actionButtons(row.attendanceId)
            }
        ]
      
    });
}

$(document).on("change", "#ddlEmployees", function () {

    var selectedId = $(this).val();

    if (selectedId) {
        loadDataTable(selectedId);
    } else {
        loadDataTable(); // show all
    }
});

$('#attendanceForm').on('submit', function (e) {

    e.preventDefault(); 

    const payload = {
        fkUserId: parseInt($('#ddlEmployees').val()),

        timeIn: $('#TimeIn').val(),
        breakIn: $('#BreakIn').val(),
        breakOut: $('#BreakOut').val(),
        timeOut: $('#TimeOut').val(),

        present: $('#Present').is(':checked'),
        absent: $('#Absent').is(':checked'),

        isManual: $('#IsManual').is(':checked')
    };

    console.log(payload); // debug

    $.ajax({
        url: '/EmployeeAttendance/AddAttendance',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(payload),

        success: function (res) {

            alert(res.message);

            $('#attendanceForm')[0].reset();
            
            loadDataTable();
        },
        error: function (err) {
            console.log(err);
            alert('Failed to save attendance');
        }
    });

});

function actionButtons(id) {
    return `
        <div class="btn-group">
            <button class="btn btn-sm btn-primary edit-btn" style="background:none; color:#0d6efd; border:none; outline:none;">
                <i class="fa fa-pencil"></i>
            </button>

            <button class="btn btn-sm btn-success save-btn d-none" style="color: green; border:none outline:none;"
                    data-id="${id}">
                <i class="fa fa-save"></i>
            </button>

            <button class="btn btn-sm btn-danger delete-btn" style="background:none; color:red; border:none; outline:none;"
                    onclick="deleteAttendance(${id})">
                <i class="fa fa-trash"></i>
            </button>
        </div>`;
}


function editRow(btn) {

    const table = $('#tblAttendance').DataTable();
    const row = table.row($(btn).closest('tr')).node();

    $(row).find('td').each(function (index) {

        // Skip FKUserId, UserName & Actions
        if (index === 0 || index === 1 || index === 2) return;

        const cell = $(this);

        // Checkbox column
        if (cell.find('input[type="checkbox"]').length) {
            cell.find('input').css('pointer-events', 'auto');
        }
        else {
            const value = cell.text().trim();

            cell.html(`
                <input type="time"
                       class="form-control form-control-sm"
                       value="${value !== '-' ? value : ''}" />
            `);
        }
    });

    // toggle buttons
    $(btn).addClass('d-none');
    $(btn).siblings('.save-btn').removeClass('d-none');
}   
//function editRow(btn) {
//    const row = $(btn).closest('tr');

//    row.find('td').each(function (index) {
//        // Skip FKUserId, UserName & Action
//        if (index === 0 || index === 1 || index === 8) return;

//        const cell = $(this);

//        // Checkboxes
//        if (cell.find('input[type="checkbox"]').length) {
//            cell.find('input').css('pointer-events', 'auto');
//        }
//        else {
//            const value = cell.text().trim();
//            cell.html(`<input type="time" class="form-control form-control-sm"  value="${value !== '-' ? value : ''}" />`);
//        }
//    });

//    $(btn).addClass('d-none');                
//    $(btn).siblings('.save-btn').removeClass('d-none'); 
//}

function saveRow(btn) {

    const row = $(btn).closest('tr');
    const table = $('#tblAttendance').DataTable();
    const rowData = table.row(row).data();

    const cells = row.find('td');

    const payload = {
        attendanceId: rowData.attendanceId,
        fkUserId: rowData.fkUserId,

        timeIn: formatTimeToDateTime($(cells[2]).find('input').val()),
        timeOut: formatTimeToDateTime($(cells[3]).find('input').val()),
        breakIn: formatTimeToDateTime($(cells[4]).find('input').val()),
        breakOut: formatTimeToDateTime($(cells[5]).find('input').val()),

        isManual: $(cells[6]).find('input').is(':checked')
    };
    

    $.ajax({
        url: '/EmployeeAttendance/UpdateAttendance',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(payload),

        success: function () {
            table.ajax.reload(null, false);
            alert('Attendance Updated ✅');
        },
        error: function () {
            alert('Update Failed ❌');
        }
    });
}

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

function formatTimeToDateTime(time) {
    if (!time) return null;

    const today = new Date().toISOString().split('T')[0];
    return `${today}T${time}:00`;
}


