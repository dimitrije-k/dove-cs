using System;
using System.Diagnostics;

namespace Dove {
	public class Key {
		private readonly byte[] key;

		public Key(byte[] newKey) {
			Debug.Assert(newKey != null && newKey.Length == 32);

			key = newKey;
		}

		public Key(string newKey) {
			Debug.Assert(newKey != null && newKey.Length == 64);

			key = Convert.FromHexString(newKey);
			Debug.Assert(key.Length == 32);
		}

		public byte this[int index] {
			get {
				return Byte(index);
			}
		}

		public byte Byte(int index) {
			return key[index];
		}

		public ushort Short(int index) {
			return BitConverter.ToUInt16(key, index * 2);
		}

		public uint Int(int index) {
			return BitConverter.ToUInt32(key, index * 4);
		}

		public ulong Long(int index) {
			return BitConverter.ToUInt64(key, index * 8);
		}
	}
}
