using System;
using System.Numerics;

namespace Dove {
	public class Auxiliaries {
		private static readonly uint[] sbox = {
			0xb7e15162, 0x8aed2a6a, 0xbf715880, 0x9cf4f3c7, 0x62e7160f, 0x38b4da56, 0xa784d904, 0x5190cfef,
			0x324e7738, 0x926cfbe5, 0xf4bf8d8d, 0x8c31d763, 0xda06c80a, 0xbb1185eb, 0x4f7c7b57, 0x57f59584,
			0x90cfd47d, 0x7c19bb42, 0x158d9554, 0xf7b46bce, 0xd55c4d79, 0xfd5f24d6, 0x613c31c3, 0x839a2ddf,
			0x8a9a276b, 0xcfbfa1c8, 0x77c56284, 0xdab79cd4, 0xc2b3293d, 0x20e9e5ea, 0xf02ac60a, 0xcc93ed87,
			0x4422a52e, 0xcb238fee, 0xe5ab6add, 0x835fd1a0, 0x753d0a8f, 0x78e537d2, 0xb95bb79d, 0x8dcaec64,
			0x2c1e9f23, 0xb829b5c2, 0x780bf387, 0x37df8bb3, 0x00d01334, 0xa0d0bd86, 0x45cbfa73, 0xa6160ffe,
			0x393c48cb, 0xbbca060f, 0x0ff8ec6d, 0x31beb5cc, 0xeed7f2f0, 0xbb088017, 0x163bc60d, 0xf45a0ecb,
			0x1bcd289b, 0x06cbbfea, 0x21ad08e1, 0x847f3f73, 0x78d56ced, 0x94640d6e, 0xf0d3d37b, 0xe67008e1,
			0x86d1bf27, 0x5b9b241d, 0xeb64749a, 0x47dfdfb9, 0x6632c3eb, 0x061b6472, 0xbbf84c26, 0x144e49c2,
			0xd04c324e, 0xf10de513, 0xd3f5114b, 0x8b5d374d, 0x93cb8879, 0xc7d52ffd, 0x72ba0aae, 0x7277da7b,
			0xa1b4af14, 0x88d8e836, 0xaf14865e, 0x6c37ab68, 0x76fe690b, 0x57112138, 0x2af341af, 0xe94f77bc,
			0xf06c83b8, 0xff5675f0, 0x979074ad, 0x9a787bc5, 0xb9bd4b0c, 0x5937d3ed, 0xe4c3a793, 0x96215eda,
			0xb1f57d0b, 0x5a7db461, 0xdd8f3c75, 0x540d0012, 0x1fd56e95, 0xf8c731e9, 0xc4d7221b, 0xbed0c62b,
			0xb5a87804, 0xb679a0ca, 0xa41d802a, 0x4604c311, 0xb71de3e5, 0xc6b400e0, 0x24a6668c, 0xcf2e2de8,
			0x6876e4f5, 0xc50000f0, 0xa93b3aa7, 0xe6342b30, 0x2a0a4737, 0x3b25f73e, 0x3b26d569, 0xfe2291ad,
			0x36d6a147, 0xd1060b87, 0x1a2801f9, 0x78376408, 0x2ff592d9, 0x140db1e9, 0x399df4b0, 0xe14ca8e8,
			0x8ee9110b, 0x2bd4fa98, 0xeed150ca, 0x6dd89322, 0x45ef7592, 0xc703f532, 0xce3a30cd, 0x31c070eb,
			0x36b4195f, 0xf33fb1c6, 0x6c7d70f9, 0x3918107c, 0xe2051fed, 0x33f6d1de, 0x9491c7de, 0xa6a5a442,
			0xe154c8bb, 0x6d8d0362, 0x803bc248, 0xd414478c, 0x2afb07ff, 0xe78e89b9, 0xfeca7e30, 0x60c08f0d,
			0x61f8e368, 0x01df66d1, 0xd8f9392e, 0x52caef06, 0x53199479, 0xdf2be64b, 0xbaab008c, 0xa8a06fda,
			0xce9ce704, 0x89845a08, 0x2ba36d61, 0x1e99f2fb, 0xe724246d, 0x18b54e33, 0x5cac0dd1, 0xab9dfd79,
			0x88a4b0c4, 0x558aa119, 0x417720b6, 0xe150ce2b, 0x927d48d7, 0x256e445e, 0x333cb757, 0x2b3bd00f,
			0xb2746043, 0x189cac11, 0x6cedc7e7, 0x71ae0358, 0xff752a3a, 0x6b6c79a5, 0x8a9a549b, 0x50c58706,
			0x90755c35, 0xe4e36b52, 0x9038ca73, 0x3fd1aaa8, 0xdab40133, 0xd80320e0, 0x790968c7, 0x6546b993,
			0xf6c8ff3b, 0x2542750d, 0xa1ffada7, 0xb7473178, 0x2e330ef7, 0xd92c43be, 0x1ad8c50a, 0x8eae20a5,
			0x556cbdd1, 0xf24c9997, 0x2cb03c73, 0x006f5c08, 0xa4e220e7, 0x4abc1791, 0x51412b1e, 0x2dd60a08,
			0xa11b02e8, 0xd70d7d71, 0x64583301, 0x1bf60945, 0x507f1a32, 0x721ac08a, 0xedc2661d, 0xa91839d1,
			0x46a2a4c4, 0x25c0ffb8, 0x7085f9b0, 0xe09b94b1, 0x46a9a478, 0x3908f3f2, 0x67a78c59, 0x430485ed,
			0x89205b36, 0xb66a57e7, 0x56e00652, 0x23670282, 0x87f8c1d6, 0x95df88c6, 0x0fe07528, 0xfcbe915c,
			0x7bf23382, 0xea293fa2, 0xda1577f9, 0xcac299bb, 0x7b4beeaf, 0xef9628c3, 0xebeaf871, 0x75c6a1f8,
			0xbdd07be3, 0x07fa1bfa, 0x9aeff794, 0xc19dfc36, 0x5f447527, 0xdea110f4, 0x208b941a, 0xa7d18538,
			0x0478aa52, 0x0e3fe233, 0x5a322edf, 0x147bbdb5, 0x27aa2ad3, 0xcb0f7d6e, 0xd381cd6a, 0xc35a1d24,
		};

		private static readonly ulong ln2 = 0xb17217f7d1cf79ab;

		internal static void StoreInSpan(ulong x, Span<byte> s) {
			for (int i = 0; i < 8; ++i) {
				s[i] = (byte) x;
				x >>= 8;
			}
		}

		internal static void StoreInSpan(ulong x, Span<uint> s) {
			s[0] = (uint) x;
			s[1] = (uint) (x >> 32);
		}

		internal static void StoreInSpan(uint x, Span<byte> s) {
			for (int i = 0; i < 4; ++i) {
				s[i] = (byte) x;
				x >>= 8;
			}
		}

		private static ulong ApplySBox(ulong x) {
			Span<byte> bytes = stackalloc byte[8];
			StoreInSpan(x, bytes);

			uint a = 0, b = 0;

			for (int i = 0; i < 4; ++i) {
				a += sbox[bytes[i + 0]];
				b += sbox[bytes[i + 4]];
			}

			return a | (ulong) b << 32;
		}

		private static byte[] ApplySBox(byte[] bytes) {
			for (int i = 0; i < 8; ++i) {
				uint x = 0;

				for (int j = 0; j < 4; ++j) {
					x += sbox[bytes[i * 4 + j]];
				}

				// Only suspect could be this method
				StoreInSpan(x, new Span<byte>(bytes, i * 4, 4));
			}

			return bytes;
		}

		private static ulong Interlace(ulong x) {
			Span<byte> bytes = stackalloc byte[8];
			StoreInSpan(x, bytes);

			x = 0;
			for (int i = 0; i < 4; ++i) {
				x >>= 8;
				x |= (ulong) bytes[i + 0] << 56;
				x >>= 8;
				x |= (ulong) bytes[i + 4] << 56;
			}

			return x;
		}

		private static byte[] Interlace8(byte[] bytes) {
			Span<byte> tmp = stackalloc byte[32];

			for (int i = 0; i < 4; ++i) {
				tmp[i * 4 +  0] = bytes[i +  0];
				tmp[i * 4 +  1] = bytes[i +  4];
				tmp[i * 4 +  2] = bytes[i +  8];
				tmp[i * 4 +  3] = bytes[i + 12];
				tmp[i * 4 + 16] = bytes[i + 16];
				tmp[i * 4 + 17] = bytes[i + 20];
				tmp[i * 4 + 18] = bytes[i + 24];
				tmp[i * 4 + 19] = bytes[i + 28];
			}

			tmp.CopyTo(bytes);
			return bytes;
		}

		private static byte[] Interlace16(byte[] bytes) {
			Span<byte> tmp = stackalloc byte[32];

			for (int i = 0; i < 4; ++i) {
				for (int j = 0; j < 2; ++j) {
					tmp[i * 8 + j + 0] = bytes[i * 2 + j +  0];
					tmp[i * 8 + j + 2] = bytes[i * 2 + j +  8];
					tmp[i * 8 + j + 4] = bytes[i * 2 + j + 16];
					tmp[i * 8 + j + 6] = bytes[i * 2 + j + 24];
				}
			}

			tmp.CopyTo(bytes);
			return bytes;
		}

		private static ulong Rotate(ulong x) {
			Span<uint> uints = stackalloc uint[2];
			StoreInSpan(x, uints);

			uints[0] = BitOperations.RotateLeft(uints[0], 5);
			uints[1] = BitOperations.RotateRight(uints[1], 5);

			return uints[0] | (ulong) uints[1] << 32;
		}

		internal static ulong Hash(ulong x, Key key) {
			x = ApplySBox(x) ^ key.Long(0);
			x = ApplySBox(Interlace(x)) ^ key.Long(1);
			x = ApplySBox(Rotate(x)) ^ key.Long(2);
			x = ApplySBox(Interlace(x)) ^ key.Long(3);

			return ~(x + ln2);
		}

		internal static Key NextKey(Key old) {
			var bytes = new byte[32];
			for (int i = 0; i < 32; ++i)
				bytes[i] = old[i];

			ApplySBox(bytes);
			Interlace8(bytes);
			ApplySBox(bytes);
			Interlace16(bytes);
			ApplySBox(bytes);

			return new Key(bytes);
		}
	}
}
