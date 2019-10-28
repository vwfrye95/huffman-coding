package me.victorfrye.huffmancoding;

import me.victorfrye.huffmancoding.service.DecodingService;
import me.victorfrye.huffmancoding.service.EncodingService;

import static me.victorfrye.huffmancoding.util.Constants.INPUT_FILENAME_ARGUMENT;
import static me.victorfrye.huffmancoding.util.Constants.OUTPUT_FILENAME_ARGUMENT;

import java.io.File;

public class Application {

	private final EncodingService encodingService = new EncodingService();
	private final DecodingService decodingService = new DecodingService();

  public static void main(String[] args) {
	  if (args.length != 2) {
	    System.out.println("Invalid number of arguments");
	    System.exit(1);
    }

	  File inputFile = new File(args[INPUT_FILENAME_ARGUMENT]);
	  if (!inputFile.exists()) {
	    System.out.printf("The input file '%s' does not exist",
          args[INPUT_FILENAME_ARGUMENT]);
	    System.exit(2);
    }

    File outputFile = new File(args[OUTPUT_FILENAME_ARGUMENT]);
	  if (outputFile.delete()) {
	    System.out.printf("The output file '%s' already exists and is being overwritten",
          args[OUTPUT_FILENAME_ARGUMENT]);
    }

	  run(args);
	}

  private static void run(String[] args) {
    // TODO
  }
}
