
//Creating a list of all the available words to guess
var car_companies = [
    ["volkswagen","bmw","toyota","ford","hyundai","kia"],
    ["tesla","honda","nissan","generalmotors","renault"]
];

//Setting the different variables
let answer = '';
let maxWrong = 6;
let mistakes = 0;
let guessed = [];
let wordStatus = null;

//Get a random company from the available company
function randomCompany() {
    company = car_companies[Math.floor(Math.random() * car_companies.length)];
}

//Get a random word from the list of words 
function randomWord() {
    company = Math.floor(Math.random() * car_companies.length);
    if (company == 1) {
        document.getElementById("typeComp").innerHTML = "Guess the Car Company (Good for the environment)";
    }
    else {
        document.getElementById("typeComp").innerHTML = "Guess the Car Company (Bad for the environment)";
    }
    answer = car_companies[company][Math.floor(Math.random() * car_companies[company].length)];
}

//Buttons for the user to guess the words
function generateButtons() {
    let buttonsHTML = 'abcdefghijklmnopqrstuvwxyz'.split('').map(letter =>
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