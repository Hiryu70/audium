import React from 'react';
import { FaCheck, FaTimes, FaForward } from 'react-icons/fa';
import classes from "./Guess.css";


const Guess = ({ state }) => {
  let color = '';
  let text = '';
  let icon = null;

  switch (state) {
    case 'right':
      color = 'green';
      text = 'Correct Answer!';
      icon = <FaCheck size={30} />;
      break;
    case 'wrong':
      color = 'red';
      text = 'Wrong Answer!';
      icon = <FaTimes size={30} />;
      break;
    case 'skip':
      color = 'gray';
      text = 'Skipped';
      icon = <FaForward size={30} />;
      break;
    case 'no answer':
      color = 'white';
      text = '';
      icon = <FaForward size={30} />;
      break;
    default:
      break;
  }

  return (
    <div className='answer-container' style={{ color }}>
      <p>{text}</p>
      {icon}
    </div>
  );
};

export default Guess;