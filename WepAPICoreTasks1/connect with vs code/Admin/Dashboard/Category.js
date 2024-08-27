

const url = "https://localhost:44367/api/Categories"

async function getCategory() {

    let response = await fetch(url);
    var result = await response.json();

    console.log(response);

    var container = document.getElementById("container");

    result.forEach(element =>{
        container.innerHTML += 
        `
        <tr>
            <td>${element.categoryId}</td>
            <td>${element.categoryName}</td>
            <td><img src="../images/${element.categoryImage}" alt="Category Image" class="img-fluid" style="max-width: 100px;"></td>
            <td>This is a simple card example with a modern design using Bootstrap.</td>
            <td><a class="btn btn-primary" href="EditCategory.html" onclick="store(${element.categoryId})">Edit</a></td>
        </tr>
        <!-- Add more rows as needed -->
                    
        `
       
    })

}
function store(id){
    localStorage.setItem("CategoryID",id);
}

function reset(){
    localStorage.clear();
}

getCategory();

// function reset(){
//     localStorage.clear();
// }