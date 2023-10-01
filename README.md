# Simple Binary Message Encoder

This README.md file provides an overview of a simple binary message encoding scheme designed and implemented for use in a signaling protocol. The encoding scheme is intended for passing messages between peers in a real-time communication application. This document will outline the design choices, constraints, and usage of the proposed encoding scheme.

## Problem Statement

The goal is to design and implement a binary message encoding scheme for a signaling protocol. The message model consists of the following components:

1. **Headers**: Messages can contain a variable number of headers, which are name-value pairs. Both header names and values are ASCII-encoded strings. Each header name and value can be up to 1023 bytes in length, and a message can have a maximum of 63 headers.

2. **Payload**: Messages also include a binary payload, which is limited to 256 KiB (256 * 1024 bytes).

## Design Choices

### Binary Message Structure

The binary message structure can be defined as follows:

- **Header Section**: The header section is organized as follows:
  - 1 byte is used to store the count of the headers. 
  - Each header consists of two parts: the name and the value.
  - For each header:
    - 1 byte is used to store the length of the header name.
    - ASCII-encoded header name data.
    - 1 byte is used to store the length of the header value.
    - ASCII-encoded header value data.

- **Payload Section**: The payload section is organized as follows:
  - 4 bytes are used to store the length of the payload.
  - The payload section is simply a sequence of binary data up to 256 KiB in length.

- **Overall Message Format**:
![Encoded Message Structure](https://github.com/N8hax/Simple-Binary-Message-Encoder/raw/main/Assets/Encoded_Message_Structure.svg)  

## Implementation Details

- **Encoding**: When encoding a message, the implementation will follow the binary message structure outlined above. It will traverse the headers and construct the binary representation accordingly then appending the payload.

- **Decoding**: When decoding a binary message, the implementation will parse the binary representation, extract headers and payload, and populate the `Message` object.

- **Input Validation**: The implementation will validate inputs to ensure that header names and values do not exceed the specified limits. It will also check for the maximum number of headers and the payload size.

## How to Use

Follow these steps to use the project:

1. **Install .NET 7:**
   - Before running the project, you need to have .NET 7 installed. If you haven't already, download and install .NET 7 from the official .NET website: [Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0).

2. **Clone the Repository:** 
   - Open your terminal or command prompt.
   - Navigate to the directory where you want to clone the repository.
   - Run the following command to clone the repository:
     ```
     git clone https://github.com/N8hax/Simple-Binary-Message-Encoder.git
     ```
3. **Navigate to the Project Directory:**
   - Navigate to the project directory:
     ```
     cd  Simple-Binary-Message-Encoder/SBE/SBE.UI
     ```

4. **Run the UI:**
   - Run the UI project using the following command:
     ```
     dotnet run
     ```
   - This will build and run the project.

**Program Output:**
![Project UI Output](https://github.com/N8hax/Simple-Binary-Message-Encoder/raw/main/Assets/Project%20UI%20Output%20Sample.png) 

## Conclusion

The proposed binary message encoding scheme is designed to be minimal in terms of complexity, tailored for the specific use case of real-time communication, and capable of handling the specified constraints. It provides a clean API for encoding and decoding messages and includes input validation and error handling for production-grade reliability. The design choices made aim to balance simplicity with efficiency and adherence to the problem requirements.
