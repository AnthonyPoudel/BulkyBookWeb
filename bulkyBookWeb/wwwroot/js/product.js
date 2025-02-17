var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/GetAll' },
        "columns": [
            { data: "title", width: "25%" },
            { data: "isbn", width: "15%" },
            { data: "price", width: "8%" },
            { data: "author", width: "15%" },
            { data: "category.name", width: "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert/${data}" class="btn btn-primary mx-2;">
                                <i class="bi bi-pencil-squre"></i> Edit
                            </a>
                            <a href="/Admin/Product/Delete/${data}" class="btn btn-danger mx-2">
                                <i class="bi bi-trash"></i> Delete
                            </a>
                        </div>
                    `;
                },
                "width": "25%"
            }
        ]
    });
}
    

