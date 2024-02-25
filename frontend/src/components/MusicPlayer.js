import React, { useState, useRef } from 'react';
import { Button, Progress, HStack, Text, VStack, Flex, Input, InputGroup, InputLeftElement } from '@chakra-ui/react';
import { FaPause, FaPlay } from 'react-icons/fa';
import { SearchIcon } from '@chakra-ui/icons';

const MusicPlayer = () => {
  const audioPlayer = useRef(null);
  const [isPlaying, setIsPlaying] = useState(false);
  const [progress, setProgress] = useState(0);

  const onTogglePlay = () => {
    if (isPlaying) {
      audioPlayer.current.pause();
    } else {
      audioPlayer.current.play();
    }
    setIsPlaying(!isPlaying);
  };

  const onTimeUpdate = () => {
    const { currentTime, duration } = audioPlayer.current;
    const progress = (currentTime / duration) * 100 || 0;
    setProgress(progress);
  };

  return (
    <div>
      <Flex direction={'column'} alignItems={'center'} >
        <audio ref={audioPlayer} onTimeUpdate={onTimeUpdate} src="/0.mp3" />
        <VStack mb={2} gap={0}>
          <Text fontSize='xl' fontWeight='semibold' color='green.300'>Этап 5</Text>
          <Text fontSize='md'>8 Секунд</Text>
        </VStack>
        <Progress borderWidth={2} w='full' height={6} borderRadius={3} borderColor='white' value={progress} colorScheme='green' />
        <HStack w='full' justifyContent={'space-between'}>
          <Text fontSize='lg'>0:00</Text>
          <Text fontSize='lg'>0:30</Text>
        </HStack>
        <Button width={16} height={16} colorScheme="green" borderRadius="full" p={3} bg="green.400" _hover={{ bg: "green.500" }}
          onClick={onTogglePlay}>
          {isPlaying ? <FaPause color='white' /> : <FaPlay color='white' />}
        </Button>
        <InputGroup w='xl' mt={4}>
          <InputLeftElement pointerEvents="none">
            <SearchIcon color="gray.300" />
          </InputLeftElement>
          <Input
            placeholder="Знаешь эту песню? Ищи по названию"
            variant="filled">
          </Input>
        </InputGroup>
        <HStack w='xl' mt={2} justifyContent={'space-between'}>
          <Button colorScheme='gray' color='white' bg="gray.400" _hover={{ bg: "gray.500" }}>
            ПРОПУСТИТЬ
          </Button>
          <Button colorScheme='green' color='white' bg="green.400" _hover={{ bg: "green.500" }}>
            ОТПРАВИТЬ
          </Button>
        </HStack>
      </Flex>
    </div>
  );
};

export default MusicPlayer;