﻿using System;
using System.IO;
using System.Security.Cryptography;

namespace Confuser.UnitTest {
	public static class FileUtilities {
		public static void ClearOutput(string outputFile) {
			try {
				if (File.Exists(outputFile)) {
					File.Delete(outputFile);
				}
			}
			catch (UnauthorizedAccessException) { }
			var debugSymbols = Path.ChangeExtension(outputFile, "pdb");
			try {
				if (File.Exists(debugSymbols)) {
					File.Delete(debugSymbols);
				}
			}
			catch (UnauthorizedAccessException) { }

			try {
				Directory.Delete(Path.GetDirectoryName(outputFile), false);
			}
			catch (IOException) { }
		}

		public static byte[] ComputeFileChecksum(string file) {
			if (file == null) throw new ArgumentNullException(nameof(file));
			if (!File.Exists(file)) throw new FileNotFoundException($"File: {file}");

			using (var checksum = SHA1.Create()) {
				using (var fs = File.OpenRead(file)) {
					return checksum.ComputeHash(fs);
				}
			}
		}
	}
}
