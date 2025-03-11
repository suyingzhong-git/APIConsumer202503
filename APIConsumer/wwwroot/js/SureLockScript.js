
function JSSearch() {
    var urlForSearch = "/SureLockTest/SearchByName?queryString=" + document.getElementById("txtSearch").value;
    console.log(urlForSearch);
    document.location.href = urlForSearch;
}

async function JSDeleteUseAPI() {
    const idVals = [];
    idVals[0]=Number(document.getElementById("id").value); 
    //alert(idVals[0].toString());
    const lockData = idVals.map((item) => JSON.stringify(item));
    //alert(lockData);
    console.log(lockData);
    await fetch(
        "https://surelock.vercel.app/api/products",
        {
            method: "DELETE",
            headers: {
                "Authorization": "Bearer a642ed09-deb5-4b5c-84ec-99e61a39e20c",
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: lockData
        }
      )
        .then(res => res.json())
        .then(data => console.log(data))
        .catch(error => console.error("Error:", error));
    document.location.href = "/SureLockTest/index";
}

async function JSEdit() {
    //alert(idVals[0].toString());
    const lockObj =new Object();
    lockObj.id=document.getElementById("id").value;
    lockObj.name = document.getElementById("name").value;
    lockObj.price = document.getElementById("price").value;
    lockObj.quantity = document.getElementById("quantity").value;
    const lockData=JSON.stringify(lockObj);
    //alert(lockData);
    console.log(lockData);
    await fetch(
        "https://surelock.vercel.app/api/products",
        {
            method: "PATCH",
            headers: {
                "Authorization": "Bearer a642ed09-deb5-4b5c-84ec-99e61a39e20c",
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: lockData
        }
    )
        .then(res => res.json())
        .then(data => console.log(data))
        .catch(error => console.error("Error:", error));
    document.location.href = "/SureLockTest/index";
}


