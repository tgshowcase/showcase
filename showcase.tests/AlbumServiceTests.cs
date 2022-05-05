using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace showcase.tests
{
    public class AlbumServiceTests
    {
        [Fact]
        public async Task process_input_should_return_invalid_input_message()
        {
            var expected = "please enter a valid album name and number. e.g. photo-album 1";
            var mockAlbumRepository = new Mock<IAlbumRepository>();
            var service = new AlbumService(mockAlbumRepository.Object);

            Assert.Equal(expected, await service.ProcessInput(""));
            Assert.Equal(expected, await service.ProcessInput("photo"));
            Assert.Equal(expected, await service.ProcessInput("photo-album a"));
            Assert.Equal(expected, await service.ProcessInput("photo-album 27b"));
            Assert.Equal(expected, await service.ProcessInput("photo-album -27"));
            Assert.Equal(expected, await service.ProcessInput("photo-album 2.7"));
        }

        [Fact]
        public async Task processInput_should_return_album_not_found()
        {
            var fakePhotoAlbums = new List<PhotoAlbum>();
            var mockAlbumRepository = new Mock<IAlbumRepository>();
            mockAlbumRepository.Setup(x => x.getAlbum(It.IsAny<long>())).ReturnsAsync(fakePhotoAlbums);
            var service = new AlbumService(mockAlbumRepository.Object);
            var expected = "album not found";
            var actual = await service.ProcessInput("photo-album 1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task processInput_should_return_a_single_photo_album()
        {
            var fakePhotoAlbums = new List<PhotoAlbum>();
            fakePhotoAlbums.Add(new PhotoAlbum
            {
                albumId = 1,
                id = 1,
                thumbnailUrl = "some thumbnail",
                title = "some title",
                url = "some url"
            });

            var mockAlbumRepository = new Mock<IAlbumRepository>();
            mockAlbumRepository.Setup(x => x.getAlbum(It.IsAny<long>())).ReturnsAsync(fakePhotoAlbums);
            var service = new AlbumService(mockAlbumRepository.Object);
            var expected = "[1] some title\r\n";
            var actual = await service.ProcessInput("photo-album 1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task processInput_should_return_a_two_photo_album()
        {
            var fakePhotoAlbums = new List<PhotoAlbum>();
            fakePhotoAlbums.Add(new PhotoAlbum
            {
                albumId = 1,
                id = 1,
                thumbnailUrl = "some thumbnail",
                title = "some title",
                url = "some url"
            });
            fakePhotoAlbums.Add(new PhotoAlbum
            {
                albumId = 1,
                id = 2,
                thumbnailUrl = "some other thumbnail",
                title = "some other title",
                url = "some other url"
            });

            var mockAlbumRepository = new Mock<IAlbumRepository>();
            mockAlbumRepository.Setup(x => x.getAlbum(It.IsAny<long>())).ReturnsAsync(fakePhotoAlbums);
            var service = new AlbumService(mockAlbumRepository.Object);
            var expected = "[1] some title\r\n[2] some other title\r\n";
            var actual = await service.ProcessInput("photo-album 1");

            Assert.Equal(expected, actual);
        }
    }
}