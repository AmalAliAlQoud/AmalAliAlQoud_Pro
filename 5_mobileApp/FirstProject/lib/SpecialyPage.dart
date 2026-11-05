import 'package:flutter/material.dart';
import 'SpeciallyDetails.dart';
import 'data/DoctorService.dart';
import 'data/ConectionString.dart';
import 'data/SpeciallyService.dart';

class specialypage extends StatefulWidget {
  const specialypage({super.key});

  @override
  State<specialypage> createState() => _specialypageState();
}

class _specialypageState extends State<specialypage> {
  // تعريف كلاس الخدمة مرة واحدة فقط
  final SpeciallyService speciallyService = SpeciallyService();

  late Future<List<dynamic>> Specially;

  @override
  void initState() {
    super.initState();
    Specially = speciallyService.getSpecialty();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Specially'),
        backgroundColor: Colors.deepPurple,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: Specially,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (snapshot.hasError) {
            return Center(
              child: Text('Error: ${snapshot.error}'),
            );
          }

          // تغيير التسمية لتعبر عن قائمة الأطباء
          final speciallyList = snapshot.data ?? [];

          if (speciallyList.isEmpty) {
            return const Center(
              child: Text('No Specially Found'),
            );
          }

          return GridView.builder(
            padding: const EdgeInsets.all(12),
            gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
              crossAxisCount: 2,
              crossAxisSpacing: 10,
              mainAxisSpacing: 10,
              childAspectRatio: 0.75,
            ),
            itemCount: speciallyList.length,
            itemBuilder: (context, index) {
              final specially = speciallyList[index];

              // رابط الصورة الخاص بالطبيب من قاعدة البيانات
              final String imagePath =
                  specially['imageUrl']?.toString() ?? '';

              final String imageUrl = imagePath.isEmpty
                  ? ''
                  : Uri.parse(ConectionString.dashboardUrl)
                  .resolve(imagePath)
                  .toString();

              return InkWell(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => SpeciallyDetails(
                        specialtyId: specially['id'], // معرف التخصص
                        specialtyName: specially['name'], // اسم التخصص لعرضه في العنوان
                      ),
                    ),
                  );
                },
                child: Card(
                  elevation: 4,
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(15),
                  ),
                  child: Column(
                    children: [
                      Expanded(
                        child: ClipRRect(
                          borderRadius: const BorderRadius.vertical(
                            top: Radius.circular(15),
                          ),
                          child: imageUrl.isNotEmpty
                              ? Image.network(
                            imageUrl,
                            width: double.infinity,
                            fit: BoxFit.cover,
                            errorBuilder: (context, error, stackTrace) {
                              return const Center(
                                child: Icon(
                                  Icons.person,
                                  size: 60,
                                ),
                              );
                            },
                          )
                              : const Center(
                            child: Icon(
                              Icons.person,
                              size: 60,
                            ),
                          ),
                        ),
                      ),
                      const SizedBox(height: 10),
                      // اسم الطبيب (تأكدي أن اسم الحقل في الـ API كـ 'name' أو 'fullName')
                      Text(
                        specially['name']?.toString() ?? 'Unknown name',
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.bold,
                        ),
                        maxLines: 1,
                        overflow: TextOverflow.ellipsis,
                      ),
                      const SizedBox(height: 5),
                      // تخصص الطبيب أو القسم

                      const SizedBox(height: 10),
                    ],
                  ),
                ),
              );
            },
          );
        },
      ),
    );
  }
}