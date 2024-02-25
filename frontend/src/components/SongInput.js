import React from 'react';
import { Button, HStack, Flex, Input, InputGroup, InputLeftElement } from '@chakra-ui/react';
import { SearchIcon } from '@chakra-ui/icons';

const SongInput = () => {
    return (
        <>
            <Flex direction={'column'} alignItems={'center'} >
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
        </>
    );
};

export default SongInput;