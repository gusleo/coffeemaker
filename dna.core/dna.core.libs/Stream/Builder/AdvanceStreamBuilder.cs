using dna.core.libs.Stream.Option;
using System;

namespace dna.core.libs.Stream
{
    public class AdvanceStreamBuilder<T> where T : class, IStreamEntity, new()
    {
        private StreamAdvanceOption _options;

        public AdvanceStreamBuilder() {
            _options = SetDefaulOptions();
        }
        public AdvanceStreamBuilder(StreamAdvanceOption options)
        {
            _options = options;
        }

        protected StreamAdvanceOption SetDefaulOptions()
        {
            return new StreamAdvanceOption()
            {
                FirstRowAsColumnName = false
            };
        }

        /// <summary>
        /// Create stream reader
        /// </summary>
        /// <param name="extension">file extension (.xlsx)</param>
        /// <returns></returns>
        public IStreamAdvanceReader<T> CreateReader(string extension)
        {
            IStreamAdvanceReader<T> _reader;

            switch ( extension.ToLower() )
            {
                case ".xlsx":
                    _reader = new ExcelStreamReader<T>(extension, _options);
                    break;
                default:
                    throw new NotImplementedException("Not implement builder for " + extension);


            }           
            
            return _reader;            
        }

    }
}
