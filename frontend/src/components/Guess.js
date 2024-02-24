import React from 'react';
import { FaCheck, FaTimes, FaForward } from 'react-icons/fa';
import { CloseIcon, ArrowRightIcon, CheckIcon } from '@chakra-ui/icons'
import { Box, Flex, Text } from '@chakra-ui/react'

const Guess = ({ state, text }) => {
  let backgroundColor = '';
  let borderColor = '';
  let icon = null;
  let letterSpacing = '0px';

  switch (state) {
    case 'right':
      backgroundColor = 'green.500';
      borderColor = 'green.500';
      icon = <CheckIcon boxSize={4} left={3} pos={'absolute'} />;
      break;
    case 'wrong':
      backgroundColor = 'red.500';
      borderColor = 'red.500';
      icon = <CloseIcon boxSize={4} left={3} pos={'absolute'} />;
      break;
    case 'skip':
      backgroundColor = 'gray.500';
      borderColor = 'gray.500';
      text = 'SKIPPED';
      icon = <ArrowRightIcon boxSize={4} left={3} pos={'absolute'} />;
      letterSpacing = '4px'
      break;
    case 'no answer':
      backgroundColor = 'gray.700';
      borderColor = 'gray.500';
      text = '';
      icon = '';
      break;
    default:
      break;
  }

  return (
    <div>
      <Flex w='xl' h={12} position={'relative'} justifyContent={'center'} alignItems={'center'} borderWidth={2} borderColor={borderColor} bg={backgroundColor} borderRadius='sm'>
        {icon}
        <Text align={'center'} fontSize='md' letterSpacing={letterSpacing} as='b'>{text}</Text>
      </Flex>
    </div>
  );
};

export default Guess;