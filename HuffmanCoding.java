/**
 * The main class to perform Huffman coding.
 * <p>
 * The following classes are needed to run HuffmanCoding.java:
 * CharacterFrequency.java
 * </p>
 * 
 * @author Victor W. Frye <vwfrye95@outlook.com>
 * @version 2.1
 * @since 1.0
 */
import java.io.*;

public class HuffmanCoding {

	public static void main(String[] args) {
		final int MAX_SIZE = 256;
		final byte INPUT_FILENAME = 0;
		final byte OUTPUT_FILENAME = 1;

		// If the number of command line arguments is invalid
		if (args.length < 2 && args.length > 3) {
			// Display usage error message and end process
			System.out.println("Usage:  HuffmanCoding.java [action path] [input filename] [output file name]");
			System.exit(0);
		}

		// If the input file does not exist
		File f = new File(args[INPUT_FILENAME]);
		if (!f.exists()) {
			// Display input error message and end process
			System.out.printf("The input file '%s' does not exist.\n", args[INPUT_FILENAME]);
			System.exit(0);
		}
			
		// Instantiate an array of character frequency objects
		CharacterFrequency[] cfArray = new CharacterFrequency[MAX_SIZE];
		for (int i = 0; i < MAX_SIZE; i++) {
			// Assign character frequency objects to array by ASCII value
			cfArray[i] = new CharacterFrequency((char) i);
		}
		
		// TODO: Fix IO Exceptions
		
		// Open the input file
		FileReader fr = new FileReader(args[INPUT_FILENAME]);
		int value = fr.read(); // Read a character from the input file
		
		// If the first character is not a 0, compress.
		if (value != 48) {
			
			// While not at the end of the input file
			while (value != -1) {
				// increment the character frequency count
				if (value < MAX_SIZE)
					cfArray[value].increment();
				value = fr.read(); // Read the next character
			}
			
			// TODO: Finish compression
		}
		// TODO: Else, decompress.
		else {
			
		}
	}
}
