
let url = "https://api.nytimes.com/svc/search/v2/articlesearch.json?q=environment&api-key=VlSnbe0WP7rCvVAqjsFuN05i3FuOlgSl";
let headlines = document.getElementById("nytArticles");

fetch(url).then(response => response.json()).then(data => {
    console.log(data.response.docs);
    data.response.docs.map(article => {

        let a = document.createElement("a");
        let line = document.createElement("hr");
        let text = document.createElement("p");
        let image = document.createElement("img");

        image.setAttribute('src', "https://static01.nyt.com/" + article.multimedia[0].url);
        image.setAttribute('height', "200px");
        a.setAttribute('href', article.web_url);
        a.innerHTML = article.headline.main;
        text.innerHTML = article.snippet + "<small> (From: " + article.source + ") </small>";

        headlines.appendChild(a);
        headlines.appendChild(image);
        headlines.appendChild(text);
        headlines.appendChild(line);

    })
})