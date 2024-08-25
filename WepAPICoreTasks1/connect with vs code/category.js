

const url = "https://localhost:44367/api/Categories"

async function getCategory() {

    let response = await fetch(url);
    var result = await response.json();

    console.log(response);

    var container = document.getElementById("container");

    result.forEach(element =>{
        container.innerHTML += 
        `
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-4">
                    <div class="card shadow-lg">
                        <img src="../images/${element.categoryImage}" class="card-img-top" alt="Card image">
                        <div class="card-body text-center">
                            <h3  class="card-title">${element.categoryId}</h3>
                            <h5 class="card-title">${element.categoryName}</h5>
                            <p class="card-text">This is a simple card example with a modern design using Bootstrap.</p>
                            
                            <a class="btn btn-primary" href="Product.html" onclick="store(${element.categoryId})">Store Data</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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