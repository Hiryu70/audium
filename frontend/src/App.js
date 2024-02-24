import './App.css';
import Guess from "./components/Guess";
import { guesses } from './data.js';

function App() {
  const guessesItems = guesses.map(guess => 
    <Guess state={guess.state} />
    );

  return (
    <div class="main-body">
      <div class="main-title">5 нот</div>
      {guessesItems}
    </div>
  );
}
export default App;