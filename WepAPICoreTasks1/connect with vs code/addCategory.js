const url = "https://localhost:44367/api/Categories"
debugger

let form  = document.getElementById("form");
async function save(){
    debugger
    event.preventDefault();
    let formData = new FormData(form);
    console.log(formData);
    let request = await fetch(url,{method:"POST", body: formData});
    debugger
    alert("Success  saved");
};