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
public class HuffmanCoding {

	public static void main(String[] args) {
		final int MAX_SIZE = 256;
		final byte ACTION_PATH = 0;
		final byte INPUT_FILENAME = 1;
		final byte OUTPUT_FILENAME = 2;

		// If the number of command line arguments is invalid, display usage
		// error message and end process
		if (args.length < 2 && args.length > 3) {
			System.out.println("Usage:  HuffmanCoding.java [action path] [input filename] [output file name]");
			System.exit(0);
		}

		// If the action path argument is invalid, display usage error message
		// and end process
		if (args[ACTION_PATH] != "compress" && args[ACTION_PATH] != "decompress") {
			System.out.println("Usage:  HuffmanCoding.java [action path] [input filename] [output file name]");
			System.out.println("Action can either be \'compress\' or \'decompress\'");
			System.exit(0);
		}

		// TODO: If the input file does not exist, display input error message
		// and end process

		CharacterFrequency cfArray = new CharacterFrequency[MAX_SIZE];
		for (int i = 0; i < MAX_SIZE; i++) {
			cfArray[i] = new CharacterFrequency((char) i);
		}

		// TODO: If action path argument constitutes compression begin
		// compression
		if (args[ACTION_PATH] == "compress") {

		}

		// TODO: If the action path argument is not compression, it is by
		// default decompression
		else {

		}
	}
}
