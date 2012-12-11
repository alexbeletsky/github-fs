using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Github.Tests.Integration
{
    public class FileSystemTests
    {
        [Test]
        public void should_return_true_if_file_exist()
        {
            var fileSystem = FileSystem.Open("alexanderbeletsky", "github-fs.net");

            Assert.That(fileSystem.FileExists("README.md"), Is.True);
        }

        [Test]
        public void should_return_false_if_file_does_not_exist()
        {
            var fileSystem = FileSystem.Open("alexanderbeletsky", "github-fs.net");

            Assert.That(fileSystem.FileExists("README_NotExist.md"), Is.False);
        }

        [Test]
        public void should_return_false_if_directory_name_passed_instead_of_filename()
        {
            var fileSystem = FileSystem.Open("alexanderbeletsky", "candidate");

            Assert.That(fileSystem.FileExists("src"), Is.False);
        }

        [Test]
        public void should_return_true_if_directory_exists()
        {
            var fileSystem = FileSystem.Open("alexanderbeletsky", "candidate");

            Assert.That(fileSystem.DirectoryExists("src"), Is.True);
        }

        [Test]
        public void should_return_false_if_directory_not_exist()
        {
            var fileSystem = FileSystem.Open("alexanderbeletsky", "candidate");

            Assert.That(fileSystem.DirectoryExists("src_not_exist"), Is.False);
        }

        [Test]
        public void should_return_false_if_filename_passed_instead_directory_name()
        {
            var fileSystem = FileSystem.Open("alexanderbeletsky", "candidate");

            Assert.That(fileSystem.DirectoryExists("README.md"), Is.False);
        }

        [Test]
        public void should_return_stat_for_file()
        {
            var fileSystem = FileSystem.Open("alexanderbeletsky", "candidate");
            var stat = fileSystem.Stat("README.md");

            Assert.That(stat.CreateAt, Is.Not.Null);
        }

        [Test]
        public void should_return_file_for_path()
        {
            var fileSystem = FileSystem.Open("alexanderbeletsky", "candidate");
            var contents = fileSystem.OpenPath("README.md");

            Assert.That(contents.FileContents, Is.Not.Null);
        }
    }
}
