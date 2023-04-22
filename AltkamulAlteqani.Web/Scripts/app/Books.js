function ChangeBookType() {
    var bookTypes = document.getElementById("bookTypesDropDown");
    var selectedType = bookTypes.options[bookTypes.selectedIndex].value;
    GetBooks(selectedType);
}

function GetBooks(bookTypeId) {
    ShowProgress();
    let xhr = new XMLHttpRequest();

    var url = base_url + '/api/Books?typeId=' + bookTypeId;
    xhr.open("GET", url, true);

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var bookList = JSON.parse(this.responseText);
            console.log(bookList);
            DisplayBooks(bookList);
        }

        HideProgress();
    }

    xhr.send();
}

function DisplayBooks(bookList) {
    var s = '<option value="-1">Select Book Type</option>';
    for (var i = 0; i < bookList.length; i++) {
        s += '<option value="' + bookList[i].BookId + '">' + bookList[i].Title + '</option>';
    }

    document.getElementById("booksDropDown").innerHTML = s;
}

function ShowProgress() {
    var loading = document.getElementsByClassName("loading")[0];
    loading.style.display = "block";
    var top = Math.max(window.innerHeight / 2 - loading.offsetHeight / 2, 0);
    var left = Math.max(window.innerWidth / 2 - loading.offsetWidth / 2, 0);
    loading.style.top = top + "px";
    loading.style.left = left + "px";
}

function HideProgress() {
    var loading = document.getElementsByClassName("loading")[0];
    loading.style.display = "none";
}