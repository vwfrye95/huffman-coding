/**
 * A class to identify a character and count occurrences of the character.
 * <p>
 * This class has two variables: 'character' and 'frequency'. The character
 * identifies the class while the frequency maintains a count of occurrences.
 * Constructors are included for default, character only, character and
 * frequency, and another 'CharacterFrequency' object. Comparison of
 * 'CharacterFrequency' objects is done only by the character variable.
 * </p>
 * 
 * @author Victor W. Frye <vwfrye95@outlook.com>
 * @version 2.1
 * @since 1.0
 */

public class CharacterFrequency {

	private int frequency; // counter variable of character frequency
	private char character; // identifying variable of character frequency

	// Default constructor
	public CharacterFrequency() {
		character = '\0'; // Set character to null by default
		frequency = 0; // Set frequency to zero by default
	}

	// Constructor given only character
	public CharacterFrequency(char ch) {
		character = ch; // Set character to passed value
		frequency = 0; // Set frequency to zero by default
	}

	// Constructor given both character and frequency
	public CharacterFrequency(char ch, int count) {
		character = ch; // Set character to argument 'ch' value
		setFrequency(count); // Pass argument 'count' to setFrequency() method
	}

	// Constructor given another CharacterFrequency object
	public CharacterFrequency(CharacterFrequency cf) {
		character = cf.getCharacter(); // Set character to passed argument's
		frequency = cf.getFrequency(); // Set frequency to passed argument's
	}

	// Methods to access 'frequency'
	public void setFrequency(int i) {
		// Verify that count is a positive number
		if (i >= 0)
			frequency = i;
		// Throw exception for inappropriate argument
		else
			throw new IllegalArgumentException();
	}

	public int getFrequency() {
		return frequency;
	}

	// Methods to access character variable
	public void setCharacter(char ch) {
		character = ch;
	}

	public char getCharacter() {
		return character;
	}

	// Method to increment frequency by one
	public void increment() {
		frequency++;
	}

	// Override equals() to compare two CharacterFrequency objects
	@Override
	public boolean equals(Object obj) {
		// If the object is null, return false
		if (obj == null)
			return false;
		// If the object is itself, return true
		if (obj == this)
			return true;
		// If the object is not of class CharacterFrequency, return false
		if (!(obj instanceof CharacterFrequency))
			return false;

		// Type cast the object as a CharacterFrequency
		CharacterFrequency cf = (CharacterFrequency) obj;

		// Compare the character of this to the object's character
		return character == cf.getCharacter();
	}

	// Output the class as a String in the format: 'A(65) = 7'
	@Override
	public String toString() {
		return String.format("%s(%d) = %d", character, (int) character, frequency);
	}
}