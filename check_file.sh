if [ $1 == '-s' ]
then
for i in $( ls | grep [A-Z] ); do mv -i $i `echo $i | tr 'A-Z' 'a-z'`; done
fi
if [ $1 == '-S' ]
then
for i in $( ls | grep [a-z] ); do mv -i $i `echo $i | tr 'a-z' 'A-Z'`; done
fi
echo Filenames altered
