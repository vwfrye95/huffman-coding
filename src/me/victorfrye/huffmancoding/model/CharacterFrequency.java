package me.victorfrye.huffmancoding.model;

import static java.util.Objects.hash;

/**
 * A class to identify a character and count occurrences of the character.
 * <p>
 * This class has two variables: 'character' and 'frequency'.  The character identifies the class while the frequency maintains a count of occurrences.  Constructors are included for default, character only, character + frequency, and a CharacterFrequency object.  Comparison of CharacterFrequency objects is done only by the character variable.
 * </p>
 *
 * @author Victor W. Frye <iam@victorfrye.me>
 */
public class CharacterFrequency {
  private char character;
  private int frequency;

  public CharacterFrequency() {
    this.character = '\0';
    this.frequency = 0;
  }

  public CharacterFrequency(char c) {
    this.character = c;
    this.frequency = 0;
  }

  public CharacterFrequency(char c, int i) {
    this.character = c;
    this.setFrequency(i);
  }

  public CharacterFrequency(CharacterFrequency cf) {
    this.character = cf.getCharacter();
    this.frequency = cf.getFrequency();
  }

  public char getCharacter() {
    return character;
  }

  public void setCharacter(char c) {
    this.character = c;
  }

  public CharacterFrequency character(CharacterFrequency cf) {
    this.character = cf.character;
    return this;
  }

  public int getFrequency() {
    return frequency;
  }

  public void setFrequency(int i) {
    if (i < 0) {
      throw new IllegalArgumentException();
    } else {
      this.frequency = i;
    }
  }

  public CharacterFrequency frequency(CharacterFrequency cf) {
    this.frequency = cf.frequency;
    return this;
  }

  public void increment() {
    frequency++;
  }

  @Override
  public boolean equals(Object o) {
    if (this == o) return true;
    if (!(o instanceof CharacterFrequency)) return false;
    CharacterFrequency that = (CharacterFrequency) o;
    return character == that.character;
  }

  @Override
  public int hashCode() {
    return hash(character);
  }

  @Override
  public String toString() {
    return String.format("CharacterFrequency{character=%s, frequency=%d}", character, frequency);
  }
}
