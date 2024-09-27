using System;

namespace Dove {
	public class Keychain {
		private readonly Key[] keys;

		private static void LastRound(ref ulong l, ref ulong r, Key key) {
			var hash = Auxiliaries.Hash(r, key);
			l ^= hash;
		}

		private static void Round(ref ulong l, ref ulong r, Key key) {
			LastRound(ref l, ref r, key);

			l ^= r;
			r ^= l;
			l ^= r;
		}

		public Keychain(Key key, int rounds = 16) {
			keys = new Key[rounds];

			keys[0] = key;

			for (int i = 1; i < rounds; ++i)
				keys[i] = Auxiliaries.NextKey(keys[i - 1]);
		}

		public byte[] Encrypt(byte[] plaintext) {
			ulong l = BitConverter.ToUInt64(plaintext, 0), r = BitConverter.ToUInt64(plaintext, 8);
			
			for (int i = 0; i < keys.Length - 1; ++i)
				Round(ref l, ref r, keys[i]);
			LastRound(ref l, ref r, keys[keys.Length - 1]);

			Auxiliaries.StoreInSpan(l, new Span<byte>(plaintext, 0, 8));
			Auxiliaries.StoreInSpan(r, new Span<byte>(plaintext, 8, 8));
			return plaintext;
		}

		public byte[] Decrypt(byte[] ciphertext) {
			ulong l = BitConverter.ToUInt64(ciphertext, 0), r = BitConverter.ToUInt64(ciphertext, 8);

			for (int i = keys.Length - 1; i >= 1; --i)
				Round(ref l, ref r, keys[i]);
			LastRound(ref l, ref r, keys[0]);

			Auxiliaries.StoreInSpan(l, new Span<byte>(ciphertext, 0, 8));
			Auxiliaries.StoreInSpan(r, new Span<byte>(ciphertext, 8, 8));
			return ciphertext;
		}
	}
}