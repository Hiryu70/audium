import React, { useState, useRef } from 'react';
import { Button, Progress, HStack, Text, VStack, Flex } from '@chakra-ui/react';
import { FaPause, FaPlay } from 'react-icons/fa';

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
    <>
      <Flex direction={'column'} alignItems={'center'} >
        <audio ref={audioPlayer} onTimeUpdate={onTimeUpdate} src="/0.mp3" />
        <VStack mb={2} gap={0}>
          <Text fontSize='xl' fontWeight='semibold' color='green.300'>Этап 5</Text>
          <Text fontSize='md'>8 Секунд</Text>
        </VStack>
        <Progress borderWidth={2} w='2xl' height={6} borderRadius={3} borderColor='white' value={progress} colorScheme='green' />
        <HStack w='full' justifyContent={'space-between'}>
          <Text fontSize='lg'>0:00</Text>
          <Text fontSize='lg'>0:30</Text>
        </HStack>
        <Button width={16} height={16} colorScheme="green" borderRadius="full" p={3} bg="green.400" _hover={{ bg: "green.500" }}
          onClick={onTogglePlay}>
          {isPlaying ? <FaPause color='white' /> : <FaPlay color='white' />}
        </Button>
      </Flex>
    </>
  );
};

export default MusicPlayer;