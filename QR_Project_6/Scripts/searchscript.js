function getNumbers(str) {
    if (str === "") {
        var empty = [];
        return empty;
    }
    return str.split("-").map(x => +x);
}
function addNumber(str, num) {
    var array = getNumbers(str);
    if (!array.includes(num)) {
        array.push(num);
    }
    return getNumString(array);
}
function getNumString(arr) {
    return arr.join("-");
}
function removeNumString(str, num) {
    var array = getNumbers(str);
    var index = array.indexOf(num);
    if (index > -1) {
        array.splice(index, 1);
    }
    return getNumString(array);
}


function myFilter(element) {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue, index,blocked;
    input = element;
    filter = input.value.toUpperCase();
    table = document.getElementsByTagName("tbody")[0];
    tr = table.getElementsByTagName("tr");

    index = 0;
    inputs = document.getElementsByClassName("search-input")
    for (i = 0; i < inputs.length; i++) {
        if (inputs[i] == input) {
            index = i;
        }
    }

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[index];
        if (td) {
            txtValue = td.textContent || td.innerText;

            if (tr[i].hasAttribute("data-blocked-by")) {
                blocked = tr[i].getAttribute("data-blocked-by");
            } else {
                blocked = "";
            }

            //Si el filtro lo permite
            if (txtValue.toUpperCase().indexOf(filter) > -1) {

                //Si blocked contenia el id de la columna, entonces lo borra
                if (getNumbers(blocked).indexOf(index) > -1) {
                    blocked = removeNumString(blocked, index);
                    tr[i].setAttribute("data-blocked-by", blocked);
                }
                //Solo si blocked esta vacio, lo muestra
                if (getNumbers(blocked).length === 0) {
                    tr[i].style.display = "";
                }
                //Si el filtro lo rechaza
            } else {
                //Siempre lo deja invisible
                tr[i].style.display = "none";
                blocked = addNumber(blocked, index);
                tr[i].setAttribute("data-blocked-by", blocked);
            }
        }
    }
}