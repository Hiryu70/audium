import './App.css';
import Guess from "./components/Guess";
import { guesses } from './data.js';
import { Divider, Flex, Heading, Container, VStack, Stack } from '@chakra-ui/react'
function App() {
  const guessesItems = guesses.map(guess =>
    <Guess key={guess.key} state={guess.state} text={guess.text} />
  );

  return (
    <div>
      <Container maxW='3xl'>
        <Flex flexDirection='column'>
          <VStack mt={6} mb={6} height={'6%'}>
          <Stack paddingBottom={'15px'} align={'center'}>
            <Heading size='2xl'>Пять нот</Heading>
          </Stack>
          <Divider />
          </VStack>

          <VStack mt={12} spacing='8px'>
            {guessesItems}
          </VStack>
        </Flex>
      </Container>
    </div>

  );
}
export default App;