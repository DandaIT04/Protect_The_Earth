var myGamePiece;
var myObstacles = [];
var myScore;

function startGame() {
    myGamePiece = new component(30, 30, "https://localhost:44302/images/earth-globe.png", 10, 120, "image");
    myGamePiece.gravity = 1;
    myScore = new component("30px", "Consolas", "black", 170, 80, "text");
    myGameArea.start();
}

var myGameArea = {
    canvas: document.createElement("canvas"),
    start: function () {
        this.canvas.width = 500;
        this.canvas.height = 400;
        this.context = this.canvas.getContext("2d");
        document.body.insertBefore(this.canvas, document.body.childNodes[2]);
        this.frameNo = 0;
        this.interval = setInterval(updateGameArea, 1);
    },
    clear: function () {
        this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
    }
}

function component(width, height, color, x, y, type) {
    this.type = type;
    if (type == "image") {
        this.image = new Image();
        this.image.src = color;
    }
    this.score = 0;
    this.width = width;
    this.height = height;
    this.speedX = 0;
    this.speedY = 0;
    this.x = x;
    this.y = y;
    this.gravity = 0;
    this.gravitySpeed = 0;
    this.update = function () {
        ctx = myGameArea.context;
        if (this.type == "image") {
            ctx.drawImage(this.image,
                this.x,
                this.y,
                this.width, this.height);
        }
        else if (this.type == "text") {
            ctx.font = this.width + " " + this.height;
            ctx.fillStyle = color;
            ctx.fillText(this.text, this.x, this.y);
        }

        else {
            ctx.fillStyle = color;
            ctx.fillRect(this.x, this.y, this.width, this.height);
        }
    }
    this.newPos = function () {
        this.gravitySpeed += this.gravity;
        this.x += this.speedX;
        this.y += this.speedY + this.gravitySpeed;
        this.hitBottom();
    }
    this.hitBottom = function () {
        var rockbottom = myGameArea.canvas.height - this.height;
        if (this.y > rockbottom) {
            this.y = rockbottom;
            this.gravitySpeed = 0;
        }
    }
    this.crashWith = function (otherobj) {
        var myleft = this.x;
        var myright = this.x + (this.width);
        var mytop = this.y;
        var mybottom = this.y + (this.height);
        var otherleft = otherobj.x;
        var otherright = otherobj.x + (otherobj.width);
        var othertop = otherobj.y;
        var otherbottom = otherobj.y + (otherobj.height);
        var crash = true;
        if ((mybottom < othertop) || (mytop > otherbottom) || (myright < otherleft) || (myleft > otherright)) {
            crash = false;
        }
        return crash;
    }
}

function updateGameArea() {
    var x, height, gap, minHeight, maxHeight, minGap, maxGap;
    for (i = 0; i < myObstacles.length; i += 1) {
        if (myGamePiece.crashWith(myObstacles[i])) {
            location.reload();
            window.alert("You lose! The earth is destroyed!");
            return;
        }
    }
    myGameArea.clear();
    myGameArea.frameNo += 1;
    if (myGameArea.frameNo == 1 || everyinterval(150)) {
        x = myGameArea.canvas.width;
        minHeight = 280;
        maxHeight = 300;
        height = Math.floor(Math.random() * (maxHeight - minHeight + 1) + minHeight);
        minGap = 170;
        maxGap = 180;
        gap = Math.floor(Math.random() * (maxGap - minGap + 1) + minGap);
        result = Math.floor((Math.random() * 3) + 1);
        if (result == 1) {
            // myObstacles.push(new component(10, height, "https://localhost:44302/images/earth-globe.png", x, 0, "image"));
            myObstacles.push(new component(70, 80, "https://localhost:44302/images/nestle.png", x, 330, "image"));
            // myObstacles.push(new component(10, x - height - gap, "https://localhost:44302/images/earth-globe.png", x, height + gap, "image"));
            myObstacles.push(new component(70, x - 80 - gap, "https://localhost:44302/images/nestle.png", x, height + gap, "image"));
        }

        else if (result == 2) {
            // myObstacles.push(new component(10, height, "https://localhost:44302/images/earth-globe.png", x, 0, "image"));
            myObstacles.push(new component(70, 80, "https://localhost:44302/images/cola.png", x, 330, "image"));
            // myObstacles.push(new component(10, x - height - gap, "https://localhost:44302/images/earth-globe.png", x, height + gap, "image"));
            myObstacles.push(new component(70, x - 80 - gap, "https://localhost:44302/images/cola.png", x, height + gap, "image"));
        }

        else if (result == 3) {
            // myObstacles.push(new component(10, height, "https://localhost:44302/images/earth-globe.png", x, 0, "image"));
            myObstacles.push(new component(70, 80, "https://localhost:44302/images/pepsi.png", x, 330, "image"));
            // myObstacles.push(new component(10, x - height - gap, "https://localhost:44302/images/earth-globe.png", x, height + gap, "image"));
            myObstacles.push(new component(70, x - 80 - gap, "https://localhost:44302/images/pepsi.png", x, height + gap, "image"));
        }
    }
    for (i = 0; i < myObstacles.length; i += 1) {
        myObstacles[i].x += -2;
        myObstacles[i].update();
    }
    myScore.text = "SCORE: " + myGameArea.frameNo;
    myScore.update();
    myGamePiece.newPos();
    myGamePiece.update();
}

function everyinterval(n) {
    if ((myGameArea.frameNo / n) % 1 == 0) { return true; }
    return false;
}

function accelerate(n) {
    myGamePiece.gravity = n;
}

document.addEventListener('keyup', event => {
    if (event.code === 'Space') {
        myGamePiece.gravity = 0.15;
    }
})

document.addEventListener('keydown', event => {
    if (event.code === 'Space') {
        myGamePiece.gravity = -0.15;
    }
})