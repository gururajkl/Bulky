$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        columns: [
            { data: 'title' },
            { data: 'author' },
            { data: 'listPrice' },
            { data: 'category.name' },
        ]
    });
}