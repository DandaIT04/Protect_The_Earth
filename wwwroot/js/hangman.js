//Source: https://www.youtube.com/watch?v=dgvyE1sJS3Y&ab_channel=SimonSuh


//Creating a list of all the available words to guess
var different_companies = [
    [
        ["volkswagen","bmw","toyota","ford","hyundai","kia"],
        ["tesla", "honda", "nissan", "generalmotors", "renault"]
    ],
    [
        ["shein", "victoria'ssecret", "mango", "h&m", "zara"],
        ["oliveankara","haikini","etrican","sui","sourcecollection"]
    ],
    [
        ["coca-cola", "nestle", "pepsico", "unilever", "marscorporation"],
        ["danone","nature'spathfood","kellogg's","fraser&neave","campbell"]
    ]
];

//Setting the different variables
var answer = '';
let maxWrong = 6;
let mistakes = 0;
let guessed = [];
let wordStatus = null;

//Get a random word from the list of words 
function randomWord() {
    var companyType = '';
    //Company Good or Bad
    var companyGB = 0;
    var companyGBString = '';
    company = Math.floor(Math.random() * different_companies.length);
    companyGB = Math.floor()
    if (company == 1) {
        companyType = 'Fashion';
    }
    else if (company == 2) {
        companyType = 'Food & Beverage';
    }
    else {
        companyType = 'Car'
    }
    companyGB = Math.floor(Math.random() * different_companies[company].length);
    if (companyGB == 1) {
        companyGBString = 'Good';
    }
    else {
        companyGBString = 'bad';
    }
    answer = different_companies[company][companyGB][Math.floor(Math.random() * different_companies[company][companyGB].length)];
    document.getElementById("typeComp").innerHTML = "Guess the " + companyType + " Company(" + companyGBString + " for the environment)";
}

//Buttons for the user to guess the words
function generateButtons() {
    let buttonsHTML = "abcdefghijklmnopqrstuvwxyz&-'".split('').map(letter =>
        `
      <button
        class="btn btn-lg btn-primary m-2"
        id='` + letter + `'
        onClick="handleGuess('` + letter + `')"
      >
        ` + letter + `
      </button>
    `).join('');

    document.getElementById('keyboard').innerHTML = buttonsHTML;
}

//Guess handler to determine the action to take when user guesses wrongly or correctly
function handleGuess(chosenLetter) {
    guessed.indexOf(chosenLetter) === -1 ? guessed.push(chosenLetter) : null;
    document.getElementById(chosenLetter).setAttribute('disabled', true);

    //Checks if the letter chosen by user matches the letters in the random word
    if (answer.indexOf(chosenLetter) >= 0) {
        guessedWord();
        checkIfGameWon();
    //If letter selected is wrong
    } else if (answer.indexOf(chosenLetter) === -1) {
        mistakes++;
        updateMistakes();
        checkIfGameLost();
        updateHangmanPicture();
    }
}

//function to update the picture if user guesses wrongly
function updateHangmanPicture() {
    document.getElementById('hangmanPic').src = 'https://localhost:44302/images/' + mistakes + '.jpg';
}

//Function to check whether user has managed to guess all the letters
function checkIfGameWon() {
    if (wordStatus === answer) {
        document.getElementById('keyboard').innerHTML = 'You Won!!!';
        var x = document.getElementById("resetBtn");
        x.style.display = "none";
        setTimeout(() => {
            window.location.href = '/Entertainment/AddPointsHang/';
        }, 1000);
    }
}

//Function to check whether user has maxed out the number of available guesses (6)
function checkIfGameLost() {
    if (mistakes === maxWrong) {
        document.getElementById('wordSpotlight').innerHTML = 'The answer was: ' + answer;
        document.getElementById('keyboard').innerHTML = 'You Lost!!!';
    }
}


function guessedWord() {
    wordStatus = answer.split('').map(letter => (guessed.indexOf(letter) >= 0 ? letter : " _ ")).join('');

    document.getElementById('wordSpotlight').innerHTML = wordStatus;
}

//Function to update the mistakes value element in the view
function updateMistakes() {
    document.getElementById('mistakes').innerHTML = mistakes;
}


//Function to reset the game when user clicks the reset button
function reset() {
    mistakes = 0;
    guessed = [];
    document.getElementById('hangmanPic').src = 'https://localhost:44302/images/0.jpg';

    randomWord();
    guessedWord();
    updateMistakes();
    generateButtons();
}

document.getElementById('maxWrong').innerHTML = maxWrong;

//Calling of the functions
randomWord();
generateButtons();
guessedWord();